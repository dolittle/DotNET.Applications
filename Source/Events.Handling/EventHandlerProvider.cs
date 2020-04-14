// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that can provide <see cref="AbstractEventHandlerForEventHandlerType{TEventHandler}" /> for <see cref="ICanHandleEvents" />.
    /// </summary>
    public class EventHandlerProvider : ICanProvideEventHandlers
    {
        readonly IImplementationsOf<ICanHandleEvents> _eventHandlerTypes;
        readonly IContainer _container;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProvider"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes">The <see cref="IImplementationsOf{T}" /> of <see cref="ICanHandleEvents" />.</param>
        /// <param name="container">The <see cref="IContainer" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventHandlerProvider(
            IImplementationsOf<ICanHandleEvents> eventHandlerTypes,
            IContainer container,
            ILogger<EventHandlerProvider> logger)
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
                var eventMethods = eventHandlerType
                                    .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName);
                if (eventMethods.Count(CheckHandleMethod) != eventMethods.Count())
                {
                    _logger.Warning(
                        "Could not register Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because it is some of the Handle methods are invalid",
                        eventHandlerType.ToString(),
                        typeof(ICanHandleEvents).ToString());
                    continue;
                }

                var eventHandlerMethods = eventMethods.Select(_ => new EventHandlerMethod<IEvent>(_.GetParameters()[0].ParameterType, _));
                eventHandlers.Add(new EventHandler(_container, eventHandlerId, eventHandlerType, IsPartitioned(eventHandlerType), eventHandlerMethods));
            }

            return eventHandlers;
        }

        bool IsPartitioned(Type type) =>
            !type.HasAttribute<NotPartitionedAttribute>();

        bool CheckEventHandlerAttributes(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<EventHandlerAttribute>())
            {
                _logger.Warning(
                    "Could not register Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because it is missing the '{eventHandletAttribute}' attribute.",
                    eventHandlerType.ToString(),
                    typeof(ICanHandleEvents).ToString(),
                    typeof(EventHandlerAttribute).ToString());
                return false;
            }

            return true;
        }

        bool CheckHandleMethod(MethodInfo methodInfo)
        {
            var eventHandlerType = methodInfo.DeclaringType;
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != 2
                || !typeof(IEvent).IsAssignableFrom(parameters?[0]?.ParameterType)
                || parameters?[1]?.ParameterType != typeof(EventContext))
            {
                _logger.Warning(
                    "Could not register Event Handler Method {eventHandlerMethod} in Event Handler '{eventHandler}'. It must take two parameters, the first being an event that implements '{event}' and the second being the context of the event '{eventContext}' ",
                    $"{methodInfo.Name}({string.Join(", ", parameters.Select(_ => _.ParameterType.ToString()))})",
                    eventHandlerType.ToString(),
                    typeof(IEvent).ToString(),
                    typeof(EventContext).ToString());
                return false;
            }

            return true;
        }
    }
}