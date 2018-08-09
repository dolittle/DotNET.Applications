/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Loader;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Collections;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Reflection;
using Dolittle.Serialization.Json;
using Dolittle.Types;

using Microsoft.Extensions.Logging;


namespace Dolittle.Artifacts.Tools
{
    // Todo: 
    // - Look for new features and add them to the topology. Base it off of the namespace by convention:
    //   First segment of the namespace is the concern or tier segment and will be ignored, last segment is the feature name
    //   Feature is required - no root level artifacts supported
    //
    //   When modules are set to "useModules=true", we use the second segment as the module
    //   All segments after module when enabled, or after the tier segment is a feature and every segment represents a child of the previous
    //   feature
    //
    // - When an artifact is no longer in the structure, we should display a warning saying it should be removed and a migrator might be necessary
    //   Migrator is only necessary if the solution is already in production or running in dev/stage with the structure
    //
    // - Consider having an option that can be passed in to say a base namespace. E.g. for Web projects (see Sentry) - we tend to have Features/ root folder.
    //   This configuration should then be optional and default set to empty. The MSBuild task should expose a variable that can be set in a <PropertyGroup/>
    //   The base namespace would be from the second segment - after tier segment
    //
    // - Validation of structure
    //   Types that are artifacts sitting on a Module should not be allowed - we should either warn or flatout error about these
    //   Look for duplicates on Id of features and modules - fail if duplicates
    //   
    class Program
    {
        static string NamespaceSeperator = ".";
        static IBoundedContextConfigurationManager _boundedContextConfigurationManager;
        static IArtifactsConfigurationManager _artifactsConfigurationManager;

        static readonly ArtifactType[] _artifactTypes = new ArtifactType[]
        {
            new ArtifactType { Type = typeof(ICommand), TypeName = "command", TargetPropertyExpression = a => a.Commands },
            new ArtifactType { Type = typeof(IEvent), TypeName = "event", TargetPropertyExpression = a => a.Events },
            new ArtifactType { Type = typeof(ICanProcessEvents), TypeName = "event processor", TargetPropertyExpression = a => a.EventProcessors },
            new ArtifactType { Type = typeof(IEventSource), TypeName = "event source", TargetPropertyExpression = a => a.EventSources },
            new ArtifactType { Type = typeof(IReadModel), TypeName = "read model", TargetPropertyExpression = a => a.ReadModels },
            new ArtifactType { Type = typeof(IQuery), TypeName = "query", TargetPropertyExpression = a => a.Queries }
        };

        static int Main(string[] args)
        {
            
            if (args.Length != 1)
            {
                ConsoleLogger.LogError("Error consolidating artifacts; missing argument for name of assembly to consolidate");
                return 1;
            }
            try
            {
                BoundedContextConfiguration boundedContextConfiguration;

                while(!System.Diagnostics.Debugger.IsAttached)
                {
                    System.Threading.Thread.Sleep(10);
                }
                var startTime = DateTime.UtcNow;

                var assemblyLoader = new AssemblyLoader(args[0]);
                SetupConfigurationManagers();

                var boundedContextConfigurationRetrievalResult = BoundedContextConfigurationUtilities.RetrieveConfiguration(_boundedContextConfigurationManager, out boundedContextConfiguration);

                var types = assemblyLoader
                    .GetProjectReferencedAssemblies()
                    .SelectMany(_ => _.ExportedTypes)
                    .Where(_ =>
                        _artifactTypes
                        .Any(at => at.Type.IsAssignableFrom(_)))
                    .ToArray();

                // ThrowIfArtifactWithNoModuleOrFeature(types);

                var typePaths = types
                    .Select(_ => 
                    string.Join(NamespaceSeperator, _.Namespace.Split(NamespaceSeperator).Skip(1)))
                    .Where(_ => _.Length > 0)
                    .Distinct()
                    .ToArray();
                
                // ThrowIfContainsInvalidTypePath(typePaths, boundedContextConfiguration.UseModules);
                
                var newArtifacts = 0;

                var existingArtifactPaths = new List<string>();

                if (boundedContextConfigurationRetrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.HasTopology)
                {
                    AddExistingArtifactPaths(boundedContextConfiguration, ref existingArtifactPaths);
                }

                var missingPaths = boundedContextConfigurationRetrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.NewBoundedContextConfig? 
                    typePaths // Optimization 
                    : typePaths.Where(_ => !existingArtifactPaths.Any(ap => ap == _)).ToArray();

                if (missingPaths.Any())
                    AddPathsToBoundedContextConfiguration(missingPaths, boundedContextConfigurationRetrievalResult, ref boundedContextConfiguration);
                
                var features = BoundedContextConfigurationUtilities.RetrieveFeatures(boundedContextConfiguration);

                var artifactsConfiguration = _artifactsConfigurationManager.Load();
                _artifactTypes.ForEach(artifactType =>
                    newArtifacts += HandleArtifactOfType(
                        artifactType.Type,
                        artifactsConfiguration,
                        features.ToList(),
                        types,
                        artifactType.TypeName,
                        artifactType.TargetPropertyExpression
                    )
                );

                var hasChanges = newArtifacts > 0;

                // if (hasChanges) _artifactsConfigurationManager.Save(artifactsConfiguration);

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);

                if (newArtifacts > 0) 
                    ConsoleLogger.LogInfo($"Added {newArtifacts} artifacts to the map (Took {deltaTime.TotalSeconds} seconds)");
                else 
                    ConsoleLogger.LogInfo($"No new artifacts added to the map (Took {deltaTime.TotalSeconds} seconds)");
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError("Error consolidating artifacts;");
                ConsoleLogger.LogError(ex.Message);
                return 1;
            }

            return 0;
        }

        static void ThrowIfArtifactWithNoModuleOrFeature(Type[] types)
        {
            foreach(var type in types)
            {
                var numSegments = type.Namespace.Split(NamespaceSeperator).Count();
                if (numSegments < 1) 
                    throw new InvalidArtifact(type);       
            }
        }

        static void ThrowIfContainsInvalidTypePath(string[] typePaths, bool useModules)
        {
            foreach(var path in typePaths)
            {
                var numSegments = path.Split(NamespaceSeperator).Count();
                if (useModules && numSegments < 2) 
                    throw new InvalidArtifact(path);
            }
        }

        static void SetupConfigurationManagers()
        {
            var container = new ActivatorContainer();
            var converterProviders = new FixedInstancesOf<ICanProvideConverters>(new []
            {
                new Dolittle.Concepts.Serialization.Json.ConverterProvider()
            });

            var serializer = new Serializer(container, converterProviders);
            _boundedContextConfigurationManager = new BoundedContextConfigurationManager(serializer);
            _artifactsConfigurationManager = new ArtifactsConfigurationManager(serializer);
        }

        static void AddExistingArtifactPaths(BoundedContextConfiguration boundedContextConfiguration, ref List<string> existingArtifactPaths)
        {
            if (boundedContextConfiguration.UseModules )
            {
               foreach (var module in boundedContextConfiguration.Topology.Modules)
               {
                   var paths = GetArtifactPathsFor(module.Features, module.Name);
                   existingArtifactPaths.AddRange(paths);
               }
            }
            else 
            {
                var paths = boundedContextConfiguration.Topology.Features;
                existingArtifactPaths.AddRange(GetArtifactPathsFor(paths));
            }
                
        }
        static IList<string> GetArtifactPathsFor(IEnumerable<FeatureDefinition> features, string parent = "")
        {
            var paths = new List<string>();
            features.ForEach(_ =>
            {
                var featurePath = new List<string>();
                if( !string.IsNullOrEmpty(parent) ) featurePath.Add($"{parent}");
                featurePath.Add(_.Name);
                var featurePathAsString = string.Join(NamespaceSeperator, featurePath);
                paths.Add(featurePathAsString);
                paths.AddRange(GetArtifactPathsFor(_.SubFeatures, featurePathAsString));
            });

            return paths;
        }

        static void AddPathsToBoundedContextConfiguration(string[] missingPaths, BoundedContextConfigurationUtilities.BoundedContextRetrievalResult retrievalResult, ref BoundedContextConfiguration config)
        {
            if (retrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.HasTopology)
                AddModulesAndFeatures(missingPaths, ref config);
            else
                AddFeatures(missingPaths, ref config);
        }

        static void AddModulesAndFeatures(string[] missingPaths, ref BoundedContextConfiguration config)
        {
            var modules = new List<ModuleDefinition>(config.Topology.Modules);

            foreach(var path in missingPaths)
            {
                modules.Add(GetModuleFromPath(path));
            }

            config.Topology.Modules = CollapseModules(modules.GroupBy(module => module.Name));
        }

        static void AddFeatures(string[] missingPaths, ref BoundedContextConfiguration config)
        {
            var features = new List<FeatureDefinition>(config.Topology.Features);
            foreach (var path in missingPaths)
            {
                features.Add(GetFeatureFromPath(path));
            }

            config.Topology.Features = CollapseFeatures(features.GroupBy(feature => feature.Name));
        }

        static ModuleDefinition GetModuleFromPath(string path)
        {
            var splitPath = path.Split(NamespaceSeperator);
            var moduleName = splitPath.First();
            var module = new ModuleDefinition()
            {
                Module = Guid.NewGuid(),
                Name = moduleName
            };
            
            var featurePath = string.Join(NamespaceSeperator, splitPath.Skip(1));
            if (!string.IsNullOrEmpty(featurePath))
            {
                module.Features = new List<FeatureDefinition>()
                {
                    GetFeatureFromPath(featurePath)
                };
            }

            return module;
        }
        static FeatureDefinition GetFeatureFromPath(string path)
        {
            var stringSegmentsReversed = path.Split(NamespaceSeperator).Reverse().ToArray();
            if (stringSegmentsReversed.Count() == 0) throw new Exception("Could not get feature from path");
            
            var currentFeature = new FeatureDefinition()
            {
                Feature = Guid.NewGuid(), 
                Name = stringSegmentsReversed[0]
            };
            foreach (var featureName in stringSegmentsReversed.Skip(1))
            {
                var parentFeature = new FeatureDefinition()
                {
                    Feature = Guid.NewGuid(),
                    Name = featureName,
                };
                parentFeature.SubFeatures = new List<FeatureDefinition>(){currentFeature};
                currentFeature = parentFeature;
            }

            return currentFeature;
        }

        static IList<ModuleDefinition> CollapseModules(IEnumerable<IGrouping<ModuleName, ModuleDefinition>> moduleGroups)
        {
            var modules = new List<ModuleDefinition>();

            foreach (var group in moduleGroups)
            {
                var module = group.ElementAt(0);
                var features = new List<FeatureDefinition>(module.Features);
                features.AddRange(group.Skip(1).SelectMany(_ => _.Features));
                module.Features = CollapseFeatures(features.GroupBy(_ => _.Name));

                modules.Add(module);
            }

            return modules;
        }
        static IList<FeatureDefinition> CollapseFeatures(IEnumerable<IGrouping<FeatureName, FeatureDefinition>> featureGroups)
        {
            var features = new List<FeatureDefinition>();

            foreach (var group in featureGroups)
            {
                var feature = group.ElementAt(0);
                var subFeatures = new List<FeatureDefinition>(feature.SubFeatures);
                subFeatures.AddRange(group.Skip(1));
                feature.SubFeatures = CollapseFeatures(subFeatures.GroupBy(_ => _.Name));

                features.Add(feature);
            }
            return features;
        }

        static int HandleArtifactOfType(Type artifactType, ArtifactsConfiguration artifactsConfiguration, List<FeatureDefinition> features, IEnumerable<Type> types, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = types.Where(_ => artifactType.IsAssignableFrom(_));
            artifacts.ForEach(artifact =>
            {
                var featureName = artifact.Namespace.Split(NamespaceSeperator).Last();
                var feature = features.SingleOrDefault(_ => _.Name == featureName);
                if (feature != null)
                {
                    ArtifactsByTypeDefinition artifactsByTypeDefinition;

                    if (artifactsConfiguration.Artifacts.ContainsKey(feature.Feature))
                        artifactsByTypeDefinition = artifactsConfiguration.Artifacts[feature.Feature];
                    else
                    {
                        artifactsByTypeDefinition = new ArtifactsByTypeDefinition();
                        artifactsConfiguration.Artifacts[feature.Feature] = artifactsByTypeDefinition;
                    }

                    var existingArtifacts = targetProperty.GetValue(artifactsByTypeDefinition) as IEnumerable<ArtifactDefinition>;
                    if (!existingArtifacts.Any(_ => _.Type.GetActualType() == artifact))
                    {
                        var newAndExistingArtifacts = new List<ArtifactDefinition>(existingArtifacts);
                        var artifactDefinition = new ArtifactDefinition
                        {
                            Artifact = ArtifactId.New(),
                            Generation = ArtifactGeneration.First,
                            Type = ClrType.FromType(artifact)
                        };
                        Console.WriteLine($"Adding '{artifact.Name}' as a new {typeName} artifact with identifier '{artifactDefinition.Artifact}'");
                        newAndExistingArtifacts.Add(artifactDefinition);

                        newArtifacts++;

                        targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
                    }
                }
            });
            return newArtifacts;
        }
    }
}