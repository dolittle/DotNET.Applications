// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlers"/>.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        const string HandleMethodName = "Handle";
        readonly ConcurrentDictionary<EventHandlerId, EventHandler> _eventHandlers = new ConcurrentDictionary<EventHandlerId, EventHandler>();
        readonly IContainer _container;
        readonly IEventHandlerProcessor _eventHandlerProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances.</param>
        /// <param name="eventHandlerProcessor"><see cref="IEventHandlerProcessor"/> for processing <see cref="EventHandler"/>.</param>
        public EventHandlers(IContainer container, IEventHandlerProcessor eventHandlerProcessor)
        {
            _container = container;
            _eventHandlerProcessor = eventHandlerProcessor;
        }

        /// <inheritdoc/>
        public EventHandler GetFor(EventHandlerId eventHandlerId)
        {
            ThrowIfMissingEventHandlerWithId(eventHandlerId);
            return _eventHandlers[eventHandlerId];
        }

        /// <inheritdoc/>
        public bool HasFor(EventHandlerId eventHandlerId)
        {
            return _eventHandlers.ContainsKey(eventHandlerId);
        }

        /// <inheritdoc/>
        public EventHandler Register<TEventHandler>(EventHandlerId eventHandlerId)
            where TEventHandler : ICanHandleEvents
        {
            return Register(typeof(TEventHandler), eventHandlerId);
        }

        /// <inheritdoc/>
        public EventHandler Register(Type type, EventHandlerId eventHandlerId)
        {
            ThrowIfTypeIsNotAnEventHandler(type);

            if (eventHandlerId == default || eventHandlerId.IsNotSet)
            {
                ThrowIfMissingAttributeForEventHandler(type);
                eventHandlerId = type.GetCustomAttribute<EventHandlerAttribute>().Id;
            }

            var eventMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                                    .Where(_ => _.Name == HandleMethodName && TakesExpectedParameters(_));

            var eventHandlerMethods = eventMethods.Select(_ => new EventHandlerMethod(_.GetParameters()[0].ParameterType, _));
            var eventHandler = new EventHandler(_container, eventHandlerId, type, eventHandlerMethods);
            _eventHandlers[eventHandlerId] = eventHandler;

            _eventHandlerProcessor.Start(eventHandler);

            return eventHandler;
        }

        bool TakesExpectedParameters(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            return parameters.Length == 2 &&
                    typeof(IEvent).IsAssignableFrom(parameters[0].ParameterType) &&
                    parameters[1].ParameterType == typeof(EventContext);
        }

        void ThrowIfTypeIsNotAnEventHandler(Type type)
        {
            if (!type.Implements(typeof(ICanHandleEvents)))
            {
                throw new TypeIsNotAnEventHandler(type);
            }
        }

        void ThrowIfMissingAttributeForEventHandler(Type type)
        {
            if (!type.HasAttribute<EventHandlerAttribute>())
            {
                throw new MissingAttributeForEventHandler(type);
            }
        }

        void ThrowIfMissingEventHandlerWithId(EventHandlerId eventHandlerId)
        {
            if (!HasFor(eventHandlerId))
            {
                throw new MissingEventHandlerWithId(eventHandlerId);
            }
        }
    }
}