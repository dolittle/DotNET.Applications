// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Build.Artifacts;
using Dolittle.Build.Proxies;
using Dolittle.Build.Topology;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Immutability;
using Dolittle.Reflection;
using Dolittle.Strings;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBuildTask"/> for doing the work that is needed for the
    /// SDK post build.
    /// </summary>
    public class BuildTask : ICanPerformBuildTask
    {
        readonly BuildTaskConfiguration _configuration;
        readonly IBoundedContextLoader _boundedContextLoader;
        readonly ArtifactTypes _artifactTypes;
        readonly IBuildMessages _buildMessages;
        readonly TopologyConfigurationHandler _topologyConfigurationHandler;
        readonly ArtifactsConfigurationHandler _artifactsConfigurationHandler;
        readonly ProxiesHandler _proxiesHandler;
        readonly BuildTarget _buildTarget;
        ArtifactsDiscoverer _artifactsDiscoverer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTask"/> class.
        /// </summary>
        /// <param name="buildTarget">Current <see cref="BuildTarget"/>.</param>
        /// <param name="configuration">Current <see cref="BuildTaskConfiguration"/>.</param>
        /// <param name="boundedContextLoader"><see cref="IBoundedContextLoader"/> for loading bounded-context.json.</param>
        /// <param name="artifactTypes">Known <see cref="ArtifactTypes"/>.</param>
        /// <param name="topologyConfigurationHandler"><see cref="TopologyConfigurationHandler"/> for handling topology configuration.</param>
        /// <param name="artifactsConfigurationHandler"><see cref="ArtifactsConfigurationHandler"/> for handling artifacts configuration.</param>
        /// <param name="proxiesHandler"><see cref="ProxiesHandler"/> for dealing with proxies.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for build messages.</param>
        public BuildTask(
            BuildTarget buildTarget,
            BuildTaskConfiguration configuration,
            IBoundedContextLoader boundedContextLoader,
            ArtifactTypes artifactTypes,
            TopologyConfigurationHandler topologyConfigurationHandler,
            ArtifactsConfigurationHandler artifactsConfigurationHandler,
            ProxiesHandler proxiesHandler,
            IBuildMessages buildMessages)
        {
            _configuration = configuration;
            _boundedContextLoader = boundedContextLoader;
            _artifactTypes = artifactTypes;
            _buildMessages = buildMessages;
            _topologyConfigurationHandler = topologyConfigurationHandler;
            _artifactsConfigurationHandler = artifactsConfigurationHandler;
            _proxiesHandler = proxiesHandler;
            _buildTarget = buildTarget;
        }

        /// <inheritdoc/>
        public string Message => "Generating topology, artifacts and proxies";

        /// <inheritdoc/>
        public void Perform()
        {
            var boundedContextConfig = _boundedContextLoader.Load(_configuration.BoundedContextConfigPath);
            _artifactsDiscoverer = new ArtifactsDiscoverer(_buildTarget.AssemblyContext, _artifactTypes, _buildMessages);

            var artifacts = _artifactsDiscoverer.Artifacts;

            var topology = _topologyConfigurationHandler.Build(artifacts, _configuration);

            var artifactsConfiguration = _artifactsConfigurationHandler.Build(artifacts, topology, _configuration);

            var events = artifacts.Where(_ => _artifactTypes.Where(artifactType => artifactType.TypeName == "event").First().Type.IsAssignableFrom(_));
            ValidateEvents(events);

            _topologyConfigurationHandler.Save(topology);
            _artifactsConfigurationHandler.Save(artifactsConfiguration);

            if (_configuration.GenerateProxies)
            {
                _proxiesHandler.CreateProxies(artifacts, _configuration, artifactsConfiguration);
            }
        }

        void ValidateEvents(IEnumerable<Type> events, int depthLevel = 0)
        {
            if (depthLevel >= 15)
            {
                _buildMessages.Error($"Event validation reached a too deep depth level ({depthLevel}), meaning that your events are way too complex!. Be aware of complex types on events.");
                throw new InvalidEvent();
            }

            ValidateEventsAreImmutable(events);
            ValidateEventsPropertiesMatchConstructorParameter(events);
            ValidateEventContent(events, depthLevel);
        }

        void ValidateEventsAreImmutable(IEnumerable<Type> events)
        {
            var mutableEvents = new List<Type>();
            events.ForEach(@event =>
            {
                if (!@event.IsImmutable()) mutableEvents.Add(@event);
            });

            if (mutableEvents.Count > 0)
            {
                _buildMessages.Warning("Discovered mutable events. An event should not have any settable properties");
                mutableEvents.ForEach(@event => _buildMessages.Error($"The event '{@event.FullName}' is not immutable."));

                throw new InvalidEvent();
            }
        }

        void ValidateEventsPropertiesMatchConstructorParameter(IEnumerable<Type> events)
        {
            var eventsWithoutNonDefaultConstructor = new List<Type>();
            var eventsWithConstructorParameterNameMismatch = new List<(Type @event, string propName)>();
            events.ForEach(@event =>
            {
                var eventConstructor = @event.GetNonDefaultConstructorWithGreatestNumberOfParameters();
                var publicProperties = @event.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (eventConstructor == null && publicProperties.Length > 0) eventsWithoutNonDefaultConstructor.Add(@event);
                else if (eventConstructor != null) ValidateEventPropertyAndConstructorParameterNameMatch(eventConstructor, publicProperties, @event, eventsWithConstructorParameterNameMismatch);
            });
            bool throwException = false;
            if (eventsWithoutNonDefaultConstructor.Count > 0)
            {
                throwException = true;
                _buildMessages.Error("Discovered events with state, but without a custom constructor.");
                eventsWithoutNonDefaultConstructor.ForEach(invalidEvent => _buildMessages.Error($"The event '{invalidEvent.FullName}' has properties, but does not have a custom constructor."));
            }

            if (eventsWithConstructorParameterNameMismatch.Count > 0)
            {
                throwException = true;
                _buildMessages.Error("Discovered events with incorrect constructors. All constructor parameter names should be camelCase and match the property name which it sets, which should be PascalCase");
                eventsWithConstructorParameterNameMismatch.ForEach(invalidEvent => _buildMessages.Error($"The event '{invalidEvent.@event.FullName}''s constructor with the most parameters is invalid. Expected a constructor parameter name to be '{invalidEvent.propName.ToCamelCase()}'"));
            }

            if (throwException) throw new InvalidEvent();
        }

        void ValidateEventPropertyAndConstructorParameterNameMatch(ConstructorInfo eventConstructor, PropertyInfo[] publicProperties, Type @event, IList<(Type @event, string propName)> invalidEvents)
        {
            var constructorPropertyNames = eventConstructor.GetParameters().Select(_ => _.Name);
            publicProperties.Select(_ => _.Name).ForEach(propName =>
            {
                if (!constructorPropertyNames.Any(paramName => paramName == propName.ToCamelCase()))
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
                foreach (var prop in @event.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var propType = prop.PropertyType;
                    if (propType.IsEnumerable())
                        propType = propType.GetEnumerableElementType();

                    if (propType.IsNullable())
                    {
                        invalidProperties.Add(prop);
                    }
                    else if (IsEvent(propType))
                    {
                        invalidProperties.Add(prop);
                    }
                    else if (propType.IsConcept())
                    {
                        invalidProperties.Add(prop);
                    }
                    else if (!propType.IsAPrimitiveType() && propType != typeof(Guid))
                    {
                        if (propType.Module != prop.DeclaringType.Module)
                            invalidProperties.Add(prop);
                        else ValidateEvents(new[] { propType }, depthLevel + 1);
                    }
                }
            }

            if (invalidProperties.Count > 0)
            {
                _buildMessages.Error($"Discovered events with invalid content.\n\tAn event cannot contain a Nullable type.\n\tAn event cannot contain a Concept.\n\tAn event cannot contain another Event.\n\tAn Event cannot contain complex types from other projects.\n\tAn event cannot contain a Complex Type that has a too deep type reference structure.");
                invalidProperties.ForEach(prop => _buildMessages.Error($"The property '{prop.Name}' of Type '{prop.PropertyType.FullName}' on the event '{prop.DeclaringType.FullName}' is invalid. "));

                throw new InvalidEvent();
            }
        }

        bool IsEvent(Type type)
        {
            return _artifactTypes.First(artifactType => artifactType.TypeName == "event").Type.IsAssignableFrom(type);
        }
    }
}