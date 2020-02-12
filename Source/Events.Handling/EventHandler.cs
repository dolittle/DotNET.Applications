// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents information for a specific <see cref="ICanHandleEvents"/>.
    /// </summary>
    public class EventHandler
    {
        readonly IContainer _container;
        readonly ConcurrentDictionary<Type, IEventHandlerMethod> _methods = new ConcurrentDictionary<Type, IEventHandlerMethod>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="identifier">The unique <see cref="EventHandlerId">identifier</see>.</param>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="methods"><see cref="IEnumerable{T}"/> of <see cref="EventHandlerMethod"/>.</param>
        public EventHandler(
            IContainer container,
            EventHandlerId identifier,
            Type type,
            IEnumerable<IEventHandlerMethod> methods)
        {
            _container = container;
            Type = type;
            Identifier = identifier;
            EventTypes = methods.Select(_ => _.EventType);

            ThrowIfTypeIsNotAnEventHandler();
            methods.ForEach(_ => _methods[_.EventType] = _);
        }

        /// <summary>
        /// Gets the type of <see cref="ICanHandleEvents"/>.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the unique <see cref="EventHandlerId">identifier</see>.
        /// </summary>
        public EventHandlerId Identifier { get; }

        /// <summary>
        /// Gets the type of events supported by the handler.
        /// </summary>
        public IEnumerable<Type> EventTypes { get; }

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
            var handler = _container.Get(Type) as ICanHandleEvents;
            await _methods[@event.Event.GetType()].Invoke(handler, @event).ConfigureAwait(false);
        }

        void ThrowIfTypeIsNotAnEventHandler()
        {
            if (!Type.Implements(typeof(ICanHandleEvents)))
            {
                throw new TypeIsNotAnEventHandler(Type);
            }
        }

        void ThrowIfMissingHandleMethodForEventType(CommittedEvent @event)
        {
            if (!_methods.ContainsKey(@event.Event.GetType()))
            {
                throw new MissingHandleMethodForEventType(Type, @event.Event.GetType());
            }
        }
    }
}