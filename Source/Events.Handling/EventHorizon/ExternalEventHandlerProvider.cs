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
        public ExternalEventHandlerProvider(
            IImplementationsOf<ICanHandleExternalEvents> eventHandlerTypes,
            IContainer container,
            ILogger<ExternalEventHandlerProvider> logger)
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
                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName);
                if (eventMethods.Count(CheckHandleMethod) != eventMethods.Count())
                {
                    _logger.Warning(
                        "Could not register External Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because it is some of the Handle methods are invalid",
                        eventHandlerType.ToString(),
                        typeof(ICanHandleExternalEvents).ToString());
                    continue;
                }

                var eventHandlerMethods = eventMethods.Select(_ => new EventHandlerMethod<IPublicEvent>(_.GetParameters()[0].ParameterType, _));

                eventHandlers.Add(new ExternalEventHandler(_container, eventHandlerId, eventHandlerType, scopeId, eventHandlerMethods));
            }

            return eventHandlers;
        }

        bool CheckEventHandlerAttributes(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<EventHandlerAttribute>())
            {
                _logger.Warning(
                    "Could not register External Event Handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{eventHandlerAttribute}' attribute",
                    eventHandlerType.ToString(),
                    typeof(ICanHandleExternalEvents).ToString(),
                    typeof(EventHandlerAttribute).ToString());
                return false;
            }

            if (!eventHandlerType.HasAttribute<ScopeAttribute>())
            {
                _logger.Warning(
                    "Could not register External Event Handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{scopeAttribute}' attribute",
                    eventHandlerType.ToString(),
                    typeof(ICanHandleExternalEvents).ToString(),
                    typeof(ScopeAttribute).ToString());
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
                || parameters?[1]?.ParameterType != typeof(EventContext))
            {
                _logger.Warning(
                    "Could not register the External Event Handler Method: {eventHandlerMethod} in event handler '{eventHandler}'. The method must take two parameters, the first being an event that implements: '{publicEvent}' and the second being the context of the event: '{eventContext}'",
                    $"{methodInfo.Name}({string.Join(", ", parameters.Select(_ => _.ParameterType.ToString()))})",
                    eventHandlerType.ToString(),
                    typeof(IPublicEvent).ToString(),
                    typeof(EventContext).ToString());
                return false;
            }

            return CheckEventAttributes(parameters?[0]?.ParameterType);
        }

        bool CheckEventAttributes(Type eventType)
        {
            if (!eventType.HasAttribute<ArtifactAttribute>())
            {
                _logger.Warning(
                    "Cannot have an External Event Handler on event '{event}' because it does not have an '{artifactAttribute}' attribute. External '{publicEvent}' must have an '{artifactAttribute}' attribute on the class",
                    eventType.ToString(),
                    typeof(ArtifactAttribute).ToString(),
                    typeof(IPublicEvent).ToString(),
                    typeof(ArtifactAttribute).ToString());
                return false;
            }

            return true;
        }
    }
}