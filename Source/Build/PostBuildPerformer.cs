/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Dolittle.Assemblies;
using Dolittle.Build.Artifacts;
using Dolittle.Build.Proxies;
using Dolittle.Build.Topology;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Events.Processing;
using Dolittle.Immutability;
using Dolittle.Reflection;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Strings;


namespace Dolittle.Build
{
    /// <summary>
    /// Represents a <see cref="ICanPerformPostBuildTasks"/> for doing the work that is needed for the
    /// SDK post build
    /// </summary>
    public class PostBuildPerformer : ICanPerformPostBuildTasks
    {
        private readonly PostBuildPerformerConfiguration _configuration;
        private readonly IBoundedContextLoader _boundedContextLoader;
        private readonly ArtifactTypes _artifactTypes;
        private readonly IBuildMessages _buildMessages;
        readonly TopologyConfigurationHandler _topologyConfigurationHandler;
        readonly ArtifactsConfigurationHandler _artifactsConfigurationHandler;
        readonly ProxiesHandler _proxiesHandler;
        ArtifactsDiscoverer _artifactsDiscoverer;
        EventProcessorDiscoverer _eventProcessorDiscoverer;
        private readonly BuildConfiguration _buildConfiguration;

        /// <summary>
        /// Initializes a new instance of <see cref="PostBuildPerformer"/>
        /// </summary>
        /// <param name="buildConfiguration"></param>
        /// <param name="configuration"></param>
        /// <param name="boundedContextLoader"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="buildMessages"></param>
        /// <param name="topologyConfigurationHandler"></param>
        /// <param name="artifactsConfigurationHandler"></param>
        /// <param name="proxiesHandler"></param>
        public PostBuildPerformer(
            BuildConfiguration buildConfiguration,
            PostBuildPerformerConfiguration configuration,
            IBoundedContextLoader boundedContextLoader,
            ArtifactTypes artifactTypes,
            IBuildMessages buildMessages,
            TopologyConfigurationHandler topologyConfigurationHandler,
            ArtifactsConfigurationHandler artifactsConfigurationHandler,
            ProxiesHandler proxiesHandler)
        {
            _configuration = configuration;
            _boundedContextLoader = boundedContextLoader;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
            _topologyConfigurationHandler = topologyConfigurationHandler;
            _artifactsConfigurationHandler = artifactsConfigurationHandler;
            _proxiesHandler = proxiesHandler;
            _buildConfiguration = buildConfiguration;
        }

        /// <inheritdoc/>
        public void Perform()
        {
            var clientAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(_buildConfiguration.TargetAssemblyPath);
            var boundedContextConfig = _boundedContextLoader.Load(_configuration.BoundedContextConfigPath);

            var assemblyContext = AssemblyContext.From(clientAssembly);

            _artifactsDiscoverer = new ArtifactsDiscoverer(assemblyContext, _artifactTypes, _buildMessages);
            _eventProcessorDiscoverer = new EventProcessorDiscoverer(assemblyContext, _buildMessages);

            var artifacts = _artifactsDiscoverer.Artifacts;

            var topology = _topologyConfigurationHandler.Build(artifacts, _configuration);

            var artifactsConfiguration = _artifactsConfigurationHandler.Build(artifacts, topology, _configuration);

            ValidateEventProcessors(_eventProcessorDiscoverer.GetAllEventProcessors());

            var events = artifacts.Where(_ => _artifactTypes.Where(artifactType => artifactType.TypeName == "event").First().Type.IsAssignableFrom(_));
            ValidateEvents(events);

            _topologyConfigurationHandler.Save(topology);
            _artifactsConfigurationHandler.Save(artifactsConfiguration);

            if (_configuration.GenerateProxies)
            {
                _proxiesHandler.CreateProxies(artifacts, _configuration, artifactsConfiguration);
            }
        }

        void ValidateEventProcessors(IEnumerable<MethodInfo> eventProcessors)
        {
            ThrowIfMultipleEventProcessorsWithId(eventProcessors);
        }

        void ThrowIfMultipleEventProcessorsWithId(IEnumerable<MethodInfo> eventProcessors)
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
                    _buildMessages.Error($"Found duplication of Event Processor Id '{entry.Key.Value.ToString()}'");
                    foreach (var eventProcessor in entry.Value)
                        _buildMessages.Error($"\tId: '{entry.Key.Value.ToString()}' Method Name: '{eventProcessor.Name}' Type FullName: '{eventProcessor.DeclaringType.FullName}'");
                }
                throw new DuplicateEventProcessor();
            }
        }

        void ValidateEvents(IEnumerable<Type> events, int depthLevel = 0)
        {
            if (depthLevel >= 3) 
            {
                _buildMessages.Error($"Event validation reached a too deep depth level, meaning that your events are way too complex!. Be aware of complex types on events.");
                throw new InvalidEvent("There are critical errors on events");
            }
            ValidateEventsAreImmutable(events);
            ValidateEventsPropertiesMatchConstructorParameter(events);
            ValidateEventContent(events, depthLevel);
        }

        void ValidateEventsAreImmutable(IEnumerable<Type> events)
        {
            var mutableEvents = new List<Type>();
            events.ForEach(@event => {
                if (! @event.IsImmutable()) mutableEvents.Add(@event);
            });
            if (mutableEvents.Any())
            {
                _buildMessages.Warning("Discovered mutable events. An event should not have any settable properties");
                mutableEvents.ForEach(@event => _buildMessages.Error($"The event '{@event.FullName}' is not immutable."));

                throw new InvalidEvent("There are critical errors on events");
            }
        }

        void ValidateEventsPropertiesMatchConstructorParameter(IEnumerable<Type> events)
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
                _buildMessages.Error("Discovered events with state, but without a custom constructor.");
                eventsWithoutNonDefaultConstructor.ForEach(invalidEvent => _buildMessages.Error($"The event '{invalidEvent.FullName}' has properties, but does not have a custom constructor."));
            }
            if (eventsWithConstructorParameterNameMismatch.Any())
            {
                throwException = true;
                _buildMessages.Error("Discovered events with incorrect constructors. All constructor parameter names should be camelCase and match the property name which it sets, which should be PascalCase");
                eventsWithConstructorParameterNameMismatch.ForEach(invalidEvent => _buildMessages.Error($"The event '{invalidEvent.@event.FullName}''s constructor with the most parameters is invalid. Expected a constructor parameter name to be '{invalidEvent.propName.ToCamelCase()}'"));
            }

            if (throwException) throw new InvalidEvent("There are critical errors on events");
        }


        void ValidateEventPropertyAndConstructorParameterNameMatch(ConstructorInfo eventConstructor, PropertyInfo[] publicProperties, Type @event, IList<(Type @event, string propName)> invalidEvents)
        {
            var constructorPropertyNames = eventConstructor.GetParameters().Select(_ => _.Name);
            publicProperties.Select(_ => _.Name).ForEach(propName => {
                if (! constructorPropertyNames.Any(paramName => paramName == propName.ToCamelCase())) 
                    invalidEvents.Add((@event, propName));
            });
        }

        void ValidateEventContent(IEnumerable<Type> events, int depthLevel)
        {
            ThrowIfEventsWithInvalidComplexProperties(events, depthLevel);
        }


        void ThrowIfEventsWithInvalidComplexProperties(IEnumerable<Type> events, int depthLevel)
        {
            var invalidProperties = new List<PropertyInfo>();
            foreach (var @event in events) 
            {
                var publicProperties = @event.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach(var prop in publicProperties)
                {
                    var propType = prop.PropertyType;
                    if (propType.IsEnumerable()) 
                        propType = propType.GetEnumerableElementType();
                    
                    if (propType.IsNullable()) 
                        invalidProperties.Add(prop);
                    else if (IsEvent(propType))
                        invalidProperties.Add(prop);
                    else if (propType.IsConcept()) 
                        invalidProperties.Add(prop);

                    else if (! propType.IsAPrimitiveType() && propType != typeof(Guid)) 
                    {
                        if (propType.Module != prop.DeclaringType.Module)
                            invalidProperties.Add(prop);
                        else ValidateEvents(new []{propType}, depthLevel + 1);

                    }
                }
            }
            if (invalidProperties.Any())
            {
                _buildMessages.Error($"Discovered events with invalid content.\n\tAn event cannot contain a Nullable type.\n\tAn event cannot contain a Concept.\n\tAn event cannot contain another Event.\n\tAn Event cannot contain complex types from other projects.\n\tAn event cannot contain a Complex Type that has a too deep type reference structure.");
                invalidProperties.ForEach(prop => _buildMessages.Error($"The property '{prop.Name}' of Type '{prop.PropertyType.FullName}' on the event '{prop.DeclaringType.FullName}' is invalid. "));

                throw new InvalidEvent("There are critical errors on events");
            }
            
        }
        bool IsEvent(Type type)
        {
            return _artifactTypes.Where(artifactType => artifactType.TypeName == "event").First().Type.IsAssignableFrom(type);
        }

    }
}