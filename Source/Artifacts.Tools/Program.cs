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

namespace Dolittle.Artifacts.Tools
{
    class Program
    {
        static IBoundedContextConfigurationManager _boundedContextConfigurationManager;
        static IArtifactsConfigurationManager _artifactsConfigurationManager;

        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Error consolidating artifacts; missing argument for name of assembly to consolidate");
                return 1;
            }

            try
            {
                var startTime = DateTime.UtcNow;

                var container = new ActivatorContainer();
                var converterProviders = new FixedInstancesOf<ICanProvideConverters>(new []
                {
                    new Dolittle.Concepts.Serialization.Json.ConverterProvider()
                });

                var serializer = new Serializer(container, converterProviders);
                _boundedContextConfigurationManager = new BoundedContextConfigurationManager(serializer);
                _artifactsConfigurationManager = new ArtifactsConfigurationManager(serializer);

                var assemblyLoader = new AssemblyLoader(args[0]);
                var assembly = assemblyLoader.Assembly;

                var boundedContextConfiguration = _boundedContextConfigurationManager.Load();
                var artifactsConfiguration = _artifactsConfigurationManager.Load();

                var features = new List<FeatureDefinition>(boundedContextConfiguration.Topology.Features);
                features.AddRange(boundedContextConfiguration.Topology.Modules.SelectMany(module => module.Features));

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

                var artifactTypes = new []
                {
                    new ArtifactType { Type = typeof(ICommand), TypeName = "command", TargetPropertyExpression = a => a.Commands },
                    new ArtifactType { Type = typeof(IEvent), TypeName = "event", TargetPropertyExpression = a => a.Commands },
                    new ArtifactType { Type = typeof(ICanProcessEvents), TypeName = "event processor", TargetPropertyExpression = a => a.Commands },
                    new ArtifactType { Type = typeof(IEventSource), TypeName = "event source", TargetPropertyExpression = a => a.Commands },
                    new ArtifactType { Type = typeof(IReadModel), TypeName = "read model", TargetPropertyExpression = a => a.Commands },
                    new ArtifactType { Type = typeof(IQuery), TypeName = "query", TargetPropertyExpression = a => a.Commands }
                };

                var types = assemblyLoader
                    .GetProjectReferencedAssemblies()
                    .SelectMany(_ => _.ExportedTypes)
                    .Where(_ =>
                        artifactTypes
                        .Any(at => at.Type.IsAssignableFrom(_)))
                    .ToArray();

                var newArtifacts = 0;

                while(!System.Diagnostics.Debugger.IsAttached);

                var artifactPaths = new List<string>();
                if( boundedContextConfiguration.UseModules ) 
                    boundedContextConfiguration.Topology.Modules.ForEach(_ => 
                        artifactPaths.AddRange(
                            GetArtifactPathsFor(_.Features, _.Name).Where(path => path.IndexOf(".") > 0)
                        )
                    );

                artifactPaths.AddRange(GetArtifactPathsFor(boundedContextConfiguration.Topology.Features));

                var typePaths = types.Select(_ => string.Join(".", _.Namespace.Split(".").Skip(1))).Where(_ => _.Length > 0).Distinct().ToArray();
                var missingPaths = typePaths.Where(_ => !artifactPaths.Any(ap => ap == _)).ToArray();

                artifactTypes.ForEach(artifactType =>
                    newArtifacts += HandleArtifactOfType(
                        artifactType.Type,
                        artifactsConfiguration,
                        features,
                        types,
                        artifactType.TypeName,
                        artifactType.TargetPropertyExpression
                    )
                );

                var hasChanges = newArtifacts > 0;

                if (hasChanges) _artifactsConfigurationManager.Save(artifactsConfiguration);

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);

                if (newArtifacts > 0) LogInfo($"Added {newArtifacts} artifacts to the map (Took {deltaTime.TotalSeconds} seconds)");
                else LogInfo($"No new artifacts added to the map (Took {deltaTime.TotalSeconds} seconds)");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        static IEnumerable<string> GetArtifactPathsFor(IEnumerable<FeatureDefinition> features, string parent = "")
        {
            var paths = new List<string>();
            features.ForEach(_ =>
            {
                var featurePath = new List<string>();
                if( !string.IsNullOrEmpty(parent) ) featurePath.Add($"{parent}");
                featurePath.Add(_.Name);
                var featurePathAsString = string.Join(".", featurePath);
                paths.Add(featurePathAsString);
                paths.AddRange(GetArtifactPathsFor(_.SubFeatures, featurePathAsString));
            });

            return paths;

        }

        static int HandleArtifactOfType(Type artifactType, ArtifactsConfiguration artifactsConfiguration, List<FeatureDefinition> features, IEnumerable<Type> types, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = types.Where(_ => artifactType.IsAssignableFrom(_));
            artifacts.ForEach(command =>
            {
                var featureName = command.Namespace.Split(".").Last();
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
                    if (!existingArtifacts.Any(_ => _.Type.GetActualType() == command))
                    {
                        var newAndExistingArtifacts = new List<ArtifactDefinition>(existingArtifacts);
                        var artifactDefinition = new ArtifactDefinition
                        {
                            Artifact = ArtifactId.New(),
                            Generation = ArtifactGeneration.First,
                            Type = ClrType.FromType(command)
                        };
                        Console.WriteLine($"Adding '{command.Name}' as a new {typeName} artifact with identifier '{artifactDefinition.Artifact}'");
                        newAndExistingArtifacts.Add(artifactDefinition);

                        newArtifacts++;

                        targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
                    }
                }
            });
            return newArtifacts;
        }

        static void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }

        static void LogError(string message)
        {
            Console.Error.WriteLine(message);
        }

    }
}