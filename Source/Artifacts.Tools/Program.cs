using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Collections;
using Dolittle.Commands;
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

                //while (!System.Diagnostics.Debugger.IsAttached);

                var resolver = new AssemblyResolver(args[0]);
                var assembly = resolver.Assembly;

                //var assemblyName = AssemblyLoadContext.GetAssemblyName(args[0]);
                //var assembly = Assembly.LoadFrom(args[0]);
                //var assembly = Assembly.Load(assemblyName);

                var boundedContextConfiguration = _boundedContextConfigurationManager.Load();
                var artifactsConfiguration = _artifactsConfigurationManager.Load();

                var features = new List<FeatureDefinition>(boundedContextConfiguration.Topology.Features);
                features.AddRange(boundedContextConfiguration.Topology.Modules.SelectMany(module => module.Features));

                var newArtifacts = 0;
                // Find new features and add them to the topology

                var types = assembly.ExportedTypes;
                //var types = assembly.GetExportedTypes();
                var commands = types.Where(_ => typeof(ICommand).IsAssignableFrom(_));
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

                        if (!artifactsByTypeDefinition.Commands.Any(_ => _.Type == command))
                        {
                            var artifacts = new List<ArtifactDefinition>(artifactsByTypeDefinition.Commands);
                            var artifactDefinition = new ArtifactDefinition
                            {
                                Artifact = ArtifactId.New(),
                                Generation = ArtifactGeneration.First,
                                Type = command
                            };
                            Console.WriteLine($"Adding '{command.Name}' as a new command artifact with identifier '{artifactDefinition.Artifact}'");
                            artifacts.Add(artifactDefinition);

                            newArtifacts++;

                            artifactsByTypeDefinition.Commands = artifacts;
                        }
                    }
                });

                _artifactsConfigurationManager.Save(artifactsConfiguration);

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
    }
}