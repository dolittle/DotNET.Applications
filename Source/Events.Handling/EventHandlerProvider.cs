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
                if (!HasEventHandlerAttribute(eventHandlerType))
                {
                    WarnEventHandlerMissingEventHandlerAttribute(eventHandlerType);
                    continue;
                }

                var eventHandlerId = eventHandlerType.GetCustomAttribute<EventHandlerAttribute>().Id;
                var handleMethods = eventHandlerType
                                    .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName);
                var hasInvalidHandlerMethod = false;
                foreach (var method in handleMethods)
                {
                    if (!FirstParameterIsEvent(method))
                    {
                        WarnFirstParameterIsNotEvent(method);
                        hasInvalidHandlerMethod = true;
                    }

                    if (!SecondParameterIsEventContext(method))
                    {
                        WarnSecondParameterIsNotEventContext(method);
                        hasInvalidHandlerMethod = true;
                    }
                }

                if (hasInvalidHandlerMethod)
                {
                    _logger.Warning(
                        "Could not register Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because some of the Handle methods are invalid",
                        eventHandlerType.ToString(),
                        typeof(ICanHandleEvents).ToString());
                    continue;
                }

                var eventHandlerMethods = handleMethods.Select(_ => new EventHandlerMethod<IEvent>(_.GetParameters()[0].ParameterType, _));
                eventHandlers.Add(new EventHandler(_container, eventHandlerId, eventHandlerType, IsPartitioned(eventHandlerType), eventHandlerMethods));
            }

            return eventHandlers;
        }

        bool IsPartitioned(Type type) =>
            !type.HasAttribute<NotPartitionedAttribute>();

        bool HasEventHandlerAttribute(Type eventHandlerType) => eventHandlerType.HasAttribute<EventHandlerAttribute>();

        void WarnEventHandlerMissingEventHandlerAttribute(Type eventHandlerType) =>
            _logger.Warning(
                "Could not register Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because it is missing the '{eventHandlerAttribute}' attribute",
                eventHandlerType.ToString(),
                typeof(ICanHandleEvents).ToString(),
                typeof(EventHandlerAttribute).ToString());

        bool FirstParameterIsEvent(MethodInfo methodInfo) => typeof(IEvent).IsAssignableFrom(methodInfo.GetParameters()[0]?.ParameterType);

        bool SecondParameterIsEventContext(MethodInfo methodInfo) => methodInfo.GetParameters()[1]?.ParameterType == typeof(EventContext);

        void WarnFirstParameterIsNotEvent(MethodInfo methodInfo) =>
            _logger.Warning(
                "Could not register the Event Handler Method: {eventHandlerMethod} in event handler '{eventHandler}'. The first parameter has to be an event that implements: '{event}'",
                $"{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(_ => _.ParameterType.ToString()))})",
                methodInfo.DeclaringType.ToString(),
                typeof(IEvent).ToString());

        void WarnSecondParameterIsNotEventContext(MethodInfo methodInfo) =>
            _logger.Warning(
                "Could not register the Event Handler Method: {eventHandlerMethod} in event handler '{eventHandler}'. The second parameter has to be the context of the event: '{eventContext}'",
                $"{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(_ => _.ParameterType.ToString()))})",
                methodInfo.DeclaringType.ToString(),
                typeof(EventContext).ToString());
    }
}