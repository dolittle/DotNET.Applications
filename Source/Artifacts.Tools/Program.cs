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
    // - Consider having an option that can be passed in to say a base namespace. E.g. for Web projects (see Sentry) - we tend to have Features/ root folder.
    //   This configuration should then be optional and default set to empty. The MSBuild task should expose a variable that can be set in a <PropertyGroup/>
    //   The base namespace would be from the second segment - after tier segment
    //
    class Program
    {
        internal const string NamespaceSeperator = ".";
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
                while(!System.Diagnostics.Debugger.IsAttached)
                {
                    System.Threading.Thread.Sleep(10);
                }
                SetupConfigurationManagers();

                BoundedContextConfiguration boundedContextConfiguration;                
                var startTime = DateTime.UtcNow;
                var assemblyLoader = new AssemblyLoader(args[0]);

                var artifactsConfiguration = _artifactsConfigurationManager.Load();

                var boundedContextConfigurationRetrievalResult = BoundedContextConfigurationUtilities.RetrieveConfiguration(_boundedContextConfigurationManager, out boundedContextConfiguration);
                if (boundedContextConfigurationRetrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.NewBoundedContextConfig)
                    boundedContextConfiguration.Topology = new TopologyConfiguration();
                
                var types = GetArtifactsFromAssembly(assemblyLoader);

                ThrowIfArtifactWithNoModuleOrFeature(types);

                var typePaths = ExtractTypePaths(types);
                
                ThrowIfContainsInvalidTypePath(typePaths, boundedContextConfiguration.UseModules);
                
                var newArtifacts = 0;

                var existingArtifactPaths = new List<string>();

                if (boundedContextConfigurationRetrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.HasTopology)
                    AddExistingArtifactPaths(boundedContextConfiguration, ref existingArtifactPaths);
                

                var missingPaths = boundedContextConfigurationRetrievalResult == BoundedContextConfigurationUtilities.BoundedContextRetrievalResult.NewBoundedContextConfig? 
                    typePaths
                    : typePaths.Where(_ => !existingArtifactPaths.Any(ap => ap == _)).ToArray();

                if (missingPaths.Any())
                    BoundedContextConfigurationUtilities.AddPathsToBoundedContextConfiguration(missingPaths, ref boundedContextConfiguration);

                boundedContextConfiguration.ValidateTopology();

                _artifactTypes.ForEach(artifactType =>
                    newArtifacts += HandleArtifactOfType(
                        artifactType.Type,
                        artifactsConfiguration,
                        types,
                        boundedContextConfiguration,
                        artifactType.TypeName,
                        artifactType.TargetPropertyExpression
                    )
                );

                artifactsConfiguration.ValidateArtifacts(boundedContextConfiguration, types);

                var hasChanges = newArtifacts > 0;

                if (missingPaths.Any()) _boundedContextConfigurationManager.Save(boundedContextConfiguration); 
                if (hasChanges) _artifactsConfigurationManager.Save(artifactsConfiguration);

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
        private static string[] ExtractTypePaths(Type[] types)
        {
            return types
                .Select(_ => 
                string.Join(NamespaceSeperator, _.Namespace.Split(NamespaceSeperator).Skip(1)))
                .Where(_ => _.Length > 0)
                .Distinct()
                .ToArray();
        }

        static Type[] GetArtifactsFromAssembly(AssemblyLoader assemblyLoader)
        {
            return assemblyLoader
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    _artifactTypes
                    .Any(at => at.Type.IsAssignableFrom(_)))
                .ToArray();
        }

        static void ThrowIfArtifactWithNoModuleOrFeature(Type[] types)
        {
            bool hasInvalidArtifact = false;
            foreach(var type in types)
            {
                var numSegments = type.Namespace.Split(NamespaceSeperator).Count();
                if (numSegments < 1) 
                {
                    hasInvalidArtifact = true;
                    ConsoleLogger.LogError($"Artifact {type.Name} with namespace = {type.Namespace} is invalid");
                }
            }
            if (hasInvalidArtifact) throw new InvalidArtifact();
        }

        static void ThrowIfContainsInvalidTypePath(string[] typePaths, bool useModules)
        {
            bool hasInvalidTypePath = false;
            foreach(var path in typePaths)
            {
                var numSegments = path.Split(NamespaceSeperator).Count();
                if (useModules && numSegments < 2) 
                {
                    hasInvalidTypePath = true;
                    ConsoleLogger.LogError($"Artifact with type path (a Module name + Feature names composition) {path} is invalid");
                }
                if (hasInvalidTypePath) throw new InvalidArtifact();
            }
        }

        static void AddExistingArtifactPaths(BoundedContextConfiguration boundedContextConfiguration, ref List<string> existingArtifactPaths)
        {
            if (boundedContextConfiguration.UseModules ) 
            {
               foreach (var module in boundedContextConfiguration.Topology.Modules)
                   existingArtifactPaths.AddRange(GetArtifactPathsFor(module.Features, module.Name));
            }
               
            else 
                existingArtifactPaths.AddRange(GetArtifactPathsFor(boundedContextConfiguration.Topology.Features));
            
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

        static int HandleArtifactOfType(Type artifactType, ArtifactsConfiguration artifactsConfiguration, IEnumerable<Type> types, BoundedContextConfiguration boundedContextConfiguration, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = types.Where(_ => artifactType.IsAssignableFrom(_));
            
            artifacts.ForEach(artifact =>
            {
                var feature = boundedContextConfiguration.FindMatchingFeature(artifact.Namespace);
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