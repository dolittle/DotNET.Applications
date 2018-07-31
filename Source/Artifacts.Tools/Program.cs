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
                var converterProviders = new FixedInstancesOf<ICanProvideConverters>(new[]
                {
                    new Dolittle.Concepts.Serialization.Json.ConverterProvider()
                });

                var serializer = new Serializer(container, converterProviders);
                _boundedContextConfigurationManager = new BoundedContextConfigurationManager(serializer);
                _artifactsConfigurationManager = new ArtifactsConfigurationManager(serializer);

                //while (!System.Diagnostics.Debugger.IsAttached);

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
                // - Add support for warnings - when the console spits out a string with prefix "warning:" - the MSBuild task should output it as warning in the logger
                //
                // - When an artifact is no longer in the structure, we should display a warning saying it should be removed and a migrator might be necessary
                //   Migrator is only necessary if the solution is already in production or running in dev/stage with the structure
                //
                // - Consider having an option that can be passed in to say a base namespace. E.g. for Web projects (see Sentry) - we tend to have Features/ root folder.
                //   This configuration should then be optional and default set to empty. The MSBuild task should expose a variable that can be set in a <PropertyGroup/>
                //   The base namespace would be from the second segment - after tier segment

                var types = assemblyLoader.GetProjectReferencedAssemblies().SelectMany(_ => _.ExportedTypes);
                
                var newArtifacts =0;
                
                newArtifacts += HandleArtifactOfType<ICommand>(artifactsConfiguration, features, types, "command", a => a.Commands);
                newArtifacts += HandleArtifactOfType<IEvent>(artifactsConfiguration, features, types, "event", a => a.Events);
                newArtifacts += HandleArtifactOfType<ICanProcessEvents>(artifactsConfiguration, features, types, "event processor", a => a.EventProcessors);
                newArtifacts += HandleArtifactOfType<IEventSource>(artifactsConfiguration, features, types, "event source", a => a.EventSources);
                newArtifacts += HandleArtifactOfType<IReadModel>(artifactsConfiguration, features, types, "read model", a => a.ReadModels);
                newArtifacts += HandleArtifactOfType<IQuery>(artifactsConfiguration, features, types, "query", a => a.Queries);

                var hasChanges = newArtifacts > 0;
                
                if( hasChanges ) _artifactsConfigurationManager.Save(artifactsConfiguration);

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);

                if (newArtifacts > 0) Console.WriteLine($"Added {newArtifacts} artifacts to the map (Took {deltaTime.TotalSeconds} seconds)");
                else Console.WriteLine($"No new artifacts added to the map (Took {deltaTime.TotalSeconds} seconds)");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        static int HandleArtifactOfType<T>(ArtifactsConfiguration artifactsConfiguration, List<FeatureDefinition> features, IEnumerable<Type> types, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>>> targetPropertyExpression)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var commands = types.Where(_ => typeof(T).IsAssignableFrom(_));
            commands.ForEach(command =>
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
                        var artifacts = new List<ArtifactDefinition>(existingArtifacts);
                        var artifactDefinition = new ArtifactDefinition
                        {
                            Artifact = ArtifactId.New(),
                            Generation = ArtifactGeneration.First,
                            Type = ClrType.FromType(command)
                        };
                        Console.WriteLine($"Adding '{command.Name}' as a new {typeName} artifact with identifier '{artifactDefinition.Artifact}'");
                        artifacts.Add(artifactDefinition);

                        newArtifacts++;

                        targetProperty.SetValue(artifactsByTypeDefinition, artifacts);
                    }
                }
            });
            return newArtifacts;
        }
    }
}