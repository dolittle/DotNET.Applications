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
using Dolittle.Reflection;
using Dolittle.Strings;

namespace Dolittle.Build
{
    class Program
    {
        static readonly IBuildToolLogger _logger = new BuildToolLogger();
        static TopologyConfigurationHandler _topologyConfigurationHandler;
        static ArtifactsConfigurationHandler _artifactsConfigurationHandler;
        static ProxiesHandler _proxiesHandler;
        static ArtifactsDiscoverer _artifactsDiscoverer;
        static EventProcessorDiscoverer _eventProcessorDiscoverer;
        static IHost _host;
        static DolittleArtifactTypes _artifactTypes;
        static IBoundedContextLoader _boundedContextLoader;

        internal static bool NewTopology = false;
        internal static bool NewArtifacts = false;

        static int Main(string[] args)
        {
            try
            {
                InitialSetup();
                _logger.Information("Build process started");
                var startTime = DateTime.UtcNow;
                var parsingResults = ArgumentsParser.Parse(args);

                var boundedContextConfig = _boundedContextLoader.Load(parsingResults.BoundedContextConfigRelativePath);

                var assemblyLoader = new AssemblyLoader(parsingResults.AssemblyPath);
                _artifactsDiscoverer = new ArtifactsDiscoverer(assemblyLoader, _artifactTypes, _logger);
                _eventProcessorDiscoverer = new EventProcessorDiscoverer(assemblyLoader, _logger);
                
                var artifacts = _artifactsDiscoverer.Artifacts;

                
                var topology = _topologyConfigurationHandler.Build(artifacts, parsingResults);

                var artifactsConfiguration = _artifactsConfigurationHandler.Build(artifacts, topology, parsingResults);

                ValidateEventProcessors(_eventProcessorDiscoverer.GetAllEventProcessors());
                ValidateEvents(artifacts);
                                
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
                new NullLoggerProvider()   
            });
            _host = new HostBuilder().Build(loggerFactory, true);
        }
        
        static void AssignBindings()
        {
            
            _artifactTypes = _host.Container.Get<DolittleArtifactTypes>();
            _topologyConfigurationHandler = _host.Container.Get<TopologyConfigurationHandler>();
            _artifactsConfigurationHandler = _host.Container.Get<ArtifactsConfigurationHandler>();
            _boundedContextLoader = _host.Container.Get<IBoundedContextLoader>();
        }


        static void ValidateEventProcessors(IEnumerable<MethodInfo> eventProcessors)
        {
            ThrowIfMultipleEventProcessorsWithId(eventProcessors);
        }

        static void ValidateEvents(IEnumerable<Type> artifacts)
        {
            var events = artifacts.Where(_ => _artifactTypes.ArtifactTypes.Where(artifactType => artifactType.TypeName == "event").First().Type.IsAssignableFrom(_));

            ValidateEventsAreImmutable(events);
            ValidateEventsPropertiesMatchConstructorParameter(events);
            
        }
        static void ValidateEventsAreImmutable(IEnumerable<Type> events)
        {
            var mutableEvents = new List<Type>();
            events.ForEach(@event => {
                if (! @event.IsImmutable()) mutableEvents.Add(@event);
            });
            if (mutableEvents.Any())
            {
                _logger.Warning("Discovered mutable events. An event should not have any settable properties");
                mutableEvents.ForEach(@event => _logger.Error($"The event '{@event.FullName}' is not immutable."));
            }
        }

        static void ValidateEventsPropertiesMatchConstructorParameter(IEnumerable<Type> events)
        {
            var eventsWithoutNonDefaultConstructor = new List<Type>();
            var eventsWithConstructorParameterNameMismatch = new List<(Type @event, string propName)>();
            events.ForEach(@event => {
                var eventConstructor = @event.GetNonDefaultConstructorWithGreatestNumberOfParameters();
                var publicProperties = @event.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (eventConstructor == null && publicProperties.Count() > 0) eventsWithoutNonDefaultConstructor.Add(@event);
                else if (eventConstructor != null) ValidateEventPropertyAndConstructorParameterNameMatch(eventConstructor, publicProperties, @event, eventsWithConstructorParameterNameMismatch);
            });
            bool throwException = false;
            if (eventsWithoutNonDefaultConstructor.Any())
            {
                throwException = true;
                _logger.Error("Discovered events with state, but without a custom constructor.");
                eventsWithoutNonDefaultConstructor.ForEach(invalidEvent => _logger.Error($"The event '{invalidEvent.FullName}' has properties, but does not have a custom constructor."));
            }
            if (eventsWithConstructorParameterNameMismatch.Any())
            {
                throwException = true;
                _logger.Error("Discovered events with incorrect constructors. All constructor parameter names should be camelCase and match the property name which it sets, which should be PascalCase");
                eventsWithConstructorParameterNameMismatch.ForEach(invalidEvent => _logger.Error($"The event '{invalidEvent.@event.FullName}''s constructor with the most parameters is invalid. Expected a constructor parameter name to be '{invalidEvent.propName.ToCamelCase()}'"));
            }

            if (throwException) throw new InvalidEvent("There are critical errors on events");
        }


        static void ValidateEventPropertyAndConstructorParameterNameMatch(ConstructorInfo eventConstructor, PropertyInfo[] publicProperties, Type @event, IList<(Type @event, string propName)> invalidEvents)
        {
            var constructorPropertyNames = eventConstructor.GetParameters().Select(_ => _.Name);
            publicProperties.Select(_ => _.Name).ForEach(propName => {
                if (! constructorPropertyNames.Any(paramName => paramName == propName.ToCamelCase())) 
                    invalidEvents.Add((@event, propName));
            });
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
                        _logger.Error($"\tId: '{entry.Key.Value.ToString()}' Method Name: '{eventProcessor.Name}' Type FullName: '{eventProcessor.DeclaringType.FullName}'");
                }
                throw new DuplicateEventProcessor();
            }
        }
    }
}