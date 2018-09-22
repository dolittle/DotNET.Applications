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
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Collections;

namespace Dolittle.Build
{
    class Program
    {
        static Dolittle.Logging.ILogger _logger;
        static TopologyConfigurationHandler _topologyConfigurationHandler;
        static ArtifactsConfigurationHandler _artifactsConfigurationHandler;
        static ProxiesHandler _proxiesHandler;
        static ArtifactsDiscoverer _artifactsDiscoverer;
        static EventProcessorDiscoverer _eventProcessorDiscoverer;
        static IHost _host;
        static DolittleArtifactTypes _artifactTypes;

        internal static bool NewTopology = false;
        internal static bool NewArtifacts = false;

        static int Main(string[] args)
        {
            try
            {
                while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(10);
                InitialSetup();

                _logger.Information("Build process started");
                var startTime = DateTime.UtcNow;
                var parsingResults = BuildToolArgumentsParser.Parse(args);

                var assemblyLoader = new AssemblyLoader(parsingResults.AssemblyPath);
                _artifactsDiscoverer = new ArtifactsDiscoverer(assemblyLoader, _artifactTypes, _logger);
                _eventProcessorDiscoverer = new EventProcessorDiscoverer(assemblyLoader, _logger);
                
                var artifacts = _artifactsDiscoverer.Artifacts;

                var topology = _topologyConfigurationHandler.Build(artifacts, parsingResults);

                var artifactsConfiguration = _artifactsConfigurationHandler.Build(artifacts, topology, parsingResults);

                ValidateEventProcessors(_eventProcessorDiscoverer.GetAllEventProcessors());
                                
                if (NewTopology) _topologyConfigurationHandler.Save(topology);
                if (NewArtifacts) _artifactsConfigurationHandler.Save(artifactsConfiguration);
                
                if (parsingResults.GenerateProxies)
                {
                    _proxiesHandler = _host.Container.Get<ProxiesHandler>();
                    _proxiesHandler.CreateProxies(artifacts, parsingResults, artifactsConfiguration);
                }

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
            try 
            {
            SetupHost();
            AssignBindings();
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"{ex} Error while doing initial setup");
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
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


        static void ValidateEventProcessors(IEnumerable<MethodInfo> eventProcessors)
        {
            ThrowIfMultipleEventProcessorsWithId(eventProcessors);
        }

        static void ThrowIfMultipleEventProcessorsWithId(IEnumerable<MethodInfo> eventProcessors)
        {
            var idMap = new Dictionary<EventProcessorId, MethodInfo>();
            var duplicateEventProcessors = new Dictionary<EventProcessorId, List<MethodInfo>>();
            eventProcessors.ForEach(method =>
            {
                var eventProcessorId = method.EventProcessorId();
                if (eventProcessorId.Value == null || eventProcessorId.Value.Equals(Guid.Empty))
                    throw new ArgumentException("Found a event processor with empty Id.", "eventProcessors");
                if (idMap.ContainsKey(eventProcessorId))
                {
                    if (! duplicateEventProcessors.ContainsKey(eventProcessorId))
                        duplicateEventProcessors.Add(eventProcessorId, new List<MethodInfo>(){idMap[eventProcessorId]});
                    
                    duplicateEventProcessors[eventProcessorId].Add(method);
                }
                else 
                {
                    idMap.Add(eventProcessorId, method);
                }
            });
            if (duplicateEventProcessors.Any())
            {
                foreach (var entry in duplicateEventProcessors)
                {
                    _logger.Error($"Found duplication of Event Processor Id '{entry.Key.Value.ToString()}'");
                    foreach (var eventProcessor in entry.Value)
                        _logger.Trace($"\tId: '{entry.Key.Value.ToString()} Method Name: {eventProcessor.Name} Type FullName: '{eventProcessor.DeclaringType.FullName}'");
                }
                throw new DuplicateEventProcessor();
            }
        }
    }
}