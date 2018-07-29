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

                var resolver = new AssemblyLoader(args[0]);
                var assembly = resolver.Assembly;

                var boundedContextConfiguration = _boundedContextConfigurationManager.Load();
                var artifactsConfiguration = _artifactsConfigurationManager.Load();

                var features = new List<FeatureDefinition>(boundedContextConfiguration.Topology.Features);
                features.AddRange(boundedContextConfiguration.Topology.Modules.SelectMany(module => module.Features));

                
                // Find new features and add them to the topology
                var types = assembly.ExportedTypes;
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