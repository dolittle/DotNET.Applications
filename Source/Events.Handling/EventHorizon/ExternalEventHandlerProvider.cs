// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Defines a system that can provide <see cref="AbstractEventHandlerForEventHandlerType{TEventHandler}" /> for <see cref="ICanHandleExternalEvents" />.
    /// </summary>
    public class ExternalEventHandlerProvider : ICanProvideEventHandlers
    {
        readonly IImplementationsOf<ICanHandleExternalEvents> _eventHandlerTypes;
        readonly IContainer _container;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProvider"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes">The <see cref="IImplementationsOf{T}" /> of <see cref="ICanHandleExternalEvents" />.</param>
        /// <param name="container">The <see cref="IContainer" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public ExternalEventHandlerProvider(IImplementationsOf<ICanHandleExternalEvents> eventHandlerTypes, IContainer container, ILogger logger)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _container = container;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractEventHandler> Provide()
        {
            var eventHandlers = new List<AbstractEventHandler>();
            foreach (var eventHandlerType in _eventHandlerTypes)
            {
                if (!CheckEventHandlerAttributes(eventHandlerType)) break;

                var eventHandlerId = eventHandlerType.GetCustomAttribute<EventHandlerAttribute>().Id;
                var scopeId = eventHandlerType.GetCustomAttribute<ScopeAttribute>().Id;
                var eventMethods = eventHandlerType
                                    .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName)
                                    .Where(CheckHandleMethod);

                var eventTypesAndMethods = eventMethods.Select(_ => (_.GetParameters()[0].ParameterType, _));
                var eventHandlerMethods = new List<EventHandlerMethod<IPublicEvent>>();
                foreach ((var eventType, var handleMethod) in eventTypesAndMethods)
                {
                    if (!CheckEventAttributes(eventType)) break;
                    eventHandlerMethods.Add(new EventHandlerMethod<IPublicEvent>(eventType, handleMethod));
                }

                eventHandlers.Add(new ExternalEventHandler(_container, eventHandlerId, eventHandlerType, scopeId, eventHandlerMethods));
            }

            return eventHandlers;
        }

        bool CheckEventHandlerAttributes(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<EventHandlerAttribute>())
            {
                _logger.Warning(
                    "Cannot register event handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{eventHandlerAttribute}' attribute.",
                    eventHandlerType.AssemblyQualifiedName,
                    typeof(ICanHandleExternalEvents).FullName,
                    typeof(EventHandlerAttribute).FullName);
                return false;
            }

            if (!eventHandlerType.HasAttribute<ScopeAttribute>())
            {
                _logger.Warning(
                    "Cannot register event handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{scopeAttribute}' attribute.",
                    eventHandlerType.AssemblyQualifiedName,
                    typeof(ICanHandleExternalEvents).FullName,
                    typeof(ScopeAttribute).FullName);
                return false;
            }

            return true;
        }

        bool CheckHandleMethod(MethodInfo methodInfo)
        {
            var eventHandlerType = methodInfo.DeclaringType;
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != 2
                || !typeof(IPublicEvent).IsAssignableFrom(parameters?[0]?.ParameterType)
                || parameters?[1]?.ParameterType == typeof(EventContext))
            {
                _logger.Warning(
                    "Event Handler Method {eventHandlerMethod} in event handler '{eventHandler}' must take two parameters the first being an event that implements '{publicEvent}' and the context of the event '{eventContext}' ",
                    $"{methodInfo.Name}({string.Join(", ", parameters.Select(_ => _.ParameterType.AssemblyQualifiedName))})",
                    eventHandlerType.AssemblyQualifiedName,
                    typeof(IPublicEvent).FullName,
                    typeof(EventContext).FullName);
                return false;
            }

            return true;
        }

        bool CheckEventAttributes(Type eventType)
        {
            if (!eventType.HasAttribute<ArtifactAttribute>())
            {
                _logger.Warning(
                    "Cannot have an event handler on event '{event}' because it does not have the '{artifactAttribute}' attribute",
                    eventType.AssemblyQualifiedName,
                    typeof(ArtifactAttribute).FullName);
                return false;
            }

            return true;
        }
    }
}