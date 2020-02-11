// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents information for a specific <see cref="ICanHandleEvents"/>.
    /// </summary>
    public class EventHandler
    {
        readonly IContainer _container;
        readonly Type _handlerType;
        readonly ConcurrentDictionary<Type, EventHandlerMethod> _methods = new ConcurrentDictionary<Type, EventHandlerMethod>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="handlerType"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        public EventHandler(IContainer container, Type handlerType)
        {
            ThrowIfTypeIsNotAnEventHandler();

            _container = container;
            _handlerType = handlerType;
        }

        /// <summary>
        /// Check if there is a method that can be invoked for the given <see cref="IEvent"/>.
        /// </summary>
        /// <param name="type">Type of <see cref="IEvent"/> to check for.</param>
        /// <returns>true if can invoke, false if not.</returns>
        public bool CanInvoke(Type type)
        {
            return _methods.ContainsKey(type);
        }

        /// <summary>
        /// Invoke the handler for the <see cref="CommittedEvent"/>.
        /// </summary>
        /// <param name="event"><see cref="CommittedEvent"/> to handle.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Invoke(CommittedEvent @event)
        {
            ThrowIfMissingHandleMethodForEventType(@event);
            var handler = _container.Get(_handlerType) as ICanHandleEvents;
            await _methods[@event.Event.GetType()].Invoke(handler, @event).ConfigureAwait(false);
        }

        void ThrowIfTypeIsNotAnEventHandler()
        {
            if (!_handlerType.Implements(typeof(ICanHandleEvents)))
            {
                throw new TypeIsNotAnEventHandler(_handlerType);
            }
        }

        void ThrowIfMissingHandleMethodForEventType(CommittedEvent @event)
        {
            if (!_methods.ContainsKey(@event.Event.GetType()))
            {
                throw new MissingHandleMethodForEventType(_handlerType, @event.Event.GetType());
            }
        }
    }
}