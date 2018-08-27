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
using Dolittle.Hosting;
using Dolittle.Types;
using Dolittle.Build.Proxies;

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
        static ProxiesHandler _proxiesHandler;
        static ArtifactsDiscoverer _artifactsDiscoverer;
        static IHost _host;
        static DolittleArtifactTypes _artifactTypes;

        internal static bool NewTopology = false;
        internal static bool NewArtifacts = false;

        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                _logger.Error("Error consolidating artifacts; missing argument for name of assembly to consolidate");
                return 1;
            }
            try
            {
                InitialSetup();

                _logger.Information("Build process started");
                var startTime = DateTime.UtcNow;
                _artifactsDiscoverer = new ArtifactsDiscoverer(args[0], _artifactTypes, _logger);
                
                var artifacts = _artifactsDiscoverer.Artifacts;
                
                var boundedContextConfiguration = _topologyConfigurationHandler.Build(artifacts);
                var artifactsConfiguration = _artifactsConfigurationHandler.Build(artifacts, boundedContextConfiguration);

                                
                if (NewTopology) _topologyConfigurationHandler.Save(boundedContextConfiguration);
                if (NewArtifacts) _artifactsConfigurationHandler.Save(artifactsConfiguration);
                
                //TODO: Generating the proxies should be optional
                _proxiesHandler = _host.Container.Get<ProxiesHandler>();
                _proxiesHandler.CreateProxies(artifacts, boundedContextConfiguration, artifactsConfiguration);

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);
                _logger.Information($"Finished build process. (Took {deltaTime.TotalSeconds} seconds)");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error consolidating artifacts;");
                _logger.Debug(ex.Message);
                return 1;
            }

            return 0;
        }

        static void InitialSetup()
        {
            SetupHost();
            AssignBindings();
        }

        static void SetupHost()
        {
            var loggerFactory = new LoggerFactory(new ILoggerProvider[]
            {
                new ConsoleLoggerProvider((s, l) => true, true)
            });
            _host = new HostBuilder().Build(loggerFactory, true);
        }
        
        static void AssignBindings()
        {
            _logger = _host.Container.Get<Dolittle.Logging.ILogger>();
            
            _artifactTypes = _host.Container.Get<DolittleArtifactTypes>();
            _topologyConfigurationHandler = _host.Container.Get<TopologyConfigurationHandler>();
            _artifactsConfigurationHandler = _host.Container.Get<ArtifactsConfigurationHandler>();
        }
    }
}