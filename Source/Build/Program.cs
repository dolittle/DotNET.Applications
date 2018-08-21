/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Events.Processing;
using Dolittle.Serialization.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Dolittle.Build.Topology;
using Dolittle.Build.Artifact;

namespace Dolittle.Build
{
    // Todo: 
    // - Consider having an option that can be passed in to say a base namespace. E.g. for Web projects (see Sentry) - we tend to have Features/ root folder.
    //   This configuration should then be optional and default set to empty. The MSBuild task should expose a variable that can be set in a <PropertyGroup/>
    //   The base namespace would be from the second segment - after tier segment
    //
    class Program
    {
        static Dolittle.Logging.ILogger _logger;
        static TopologyConfigurationHandler _topologyConfigurationHandler;
        static ArtifactsConfigurationHandler _artifactsConfigurationHandler;

        internal static bool NewTopology = false;
        internal static bool NewArtifacts = false;

        readonly static ArtifactType[] _artifactTypes = new ArtifactType[]
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
                _logger.Error("Error consolidating artifacts; missing argument for name of assembly to consolidate");
                return 1;
            }
            try
            {
                // For debugging, comment out or remove when not debugging
                while (!System.Diagnostics.Debugger.IsAttached)
                    System.Threading.Thread.Sleep(10);

                InitialSetup();

                _logger.Information("Build process started");
                var startTime = DateTime.UtcNow;
                var assemblyLoader = new AssemblyLoader(args[0]);
                
                var types = DiscoverArtifacts(assemblyLoader); 
                
                var boundedContextConfiguration = _topologyConfigurationHandler.Build(types);
                var artifactsConfiguration = _artifactsConfigurationHandler.Build(types, boundedContextConfiguration);
                
                if (NewTopology) _topologyConfigurationHandler.Save(boundedContextConfiguration);
                if (NewArtifacts) _artifactsConfigurationHandler.Save(artifactsConfiguration);

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);
                _logger.Information($"Finished build process. (Took {deltaTime.TotalSeconds} seconds)");
            }
            catch (Exception ex)
            {
                _logger.Error("Error consolidating artifacts;");
                _logger.Debug(ex.Message);
                return 1;
            }

            return 0;
        }

        static void InitialSetup()
        {
            SetupLogger();
            SetupHandlers();
        }
        
        static void SetupLogger()
        {
            var loggerFactory = new LoggerFactory(new ILoggerProvider[]
            {
                new ConsoleLoggerProvider((s, l) => true, true)
            });

            var appenders = Dolittle.Logging.Bootstrap.EntryPoint.Initialize(loggerFactory);
            _logger = new Dolittle.Logging.Logger(appenders);
        }
        
        static void SetupHandlers()
        {
            var container = new ActivatorContainer();
            var converterProviders = new FixedInstancesOf<ICanProvideConverters>(new ICanProvideConverters[]{});

            var serializer = new Serializer(container, converterProviders);

            _topologyConfigurationHandler = new TopologyConfigurationHandler(serializer, _logger);
            _artifactsConfigurationHandler = new ArtifactsConfigurationHandler(serializer, _artifactTypes, _logger);

        }
        static Type[] DiscoverArtifacts(AssemblyLoader assemblyLoader)
        {
            var types = GetArtifactsFromAssembly(assemblyLoader);

            ThrowIfArtifactWithNoModuleOrFeature(types);

            return types;
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
                var numSegments = type.Namespace.Split(".").Count();
                if (numSegments < 1) 
                {
                    hasInvalidArtifact = true;
                    _logger.Error($"Artifact {type.Name} with namespace = {type.Namespace} is invalid");
                }
            }
            if (hasInvalidArtifact) throw new InvalidArtifact();
        }
    }
}