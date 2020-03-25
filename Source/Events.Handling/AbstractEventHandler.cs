// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.DependencyInversion;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an abstract implementation for a system that can invoke a <see cref="CommittedEvent" /> on an event handler.
    /// </summary>
    public abstract class AbstractEventHandler
    {
        /// <summary>
        /// The method name of the Event Handler method that can handle an Event.
        /// </summary>
        public const string HandleMethodName = "Handle";

        readonly IContainer _container;
        readonly ConcurrentDictionary<Type, IEventHandlerMethod> _methods = new ConcurrentDictionary<Type, IEventHandlerMethod>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEventHandler"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="identifier">The unique <see cref="EventHandlerId">identifier</see>.</param>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="partitioned">Whether the Event Handler is partitioned.</param>
        /// <param name="methods"><see cref="IEnumerable{T}"/> of <see cref="EventHandlerMethod{T}"/>.</param>
        protected AbstractEventHandler(
            IContainer container,
            ScopeId scope,
            EventHandlerId identifier,
            Type type,
            bool partitioned,
            IEnumerable<IEventHandlerMethod> methods)
        {
            _container = container;
            Type = type;
            Scope = scope;
            Identifier = identifier;
            Partitioned = partitioned;
            EventTypes = methods.Select(_ => _.EventType);
            methods.ForEach(_ => _methods[_.EventType] = _);
        }

        /// <summary>
        /// Gets the <see cref="ScopeId" />.
        /// </summary>
        public ScopeId Scope { get; }

        /// <summary>
        /// Gets the unique <see cref="EventHandlerId">identifier</see>.
        /// </summary>
        public EventHandlerId Identifier { get; }

        /// <summary>
        /// Gets a value indicating whether this Event Handler is partitioned.
        /// </summary>
        public bool Partitioned { get; }

        /// <summary>
        /// Gets the type of events supported by the handler.
        /// </summary>
        public IEnumerable<Type> EventTypes { get; }

        /// <summary>
        /// Gets the of event handler instance type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Invoke the handler for the <see cref="CommittedEvent"/>.
        /// </summary>
        /// <param name="event"><see cref="CommittedEvent"/> to handle.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Invoke(CommittedEvent @event)
        {
            ThrowIfCannotInvoke(@event);
            var handler = _container.Get(Type) as ICanHandleEvents;
            return _methods[@event.Event.GetType()].Invoke(handler, @event);
        }

        /// <summary>
        /// Check if there is a method that can be invoked for the given <see cref="CommittedEvent"/>.
        /// </summary>
        /// <param name="event">The event to check.</param>
        protected virtual void ThrowIfCannotInvoke(CommittedEvent @event)
        {
            if (!_methods.ContainsKey(@event.Event.GetType())) throw new MissingHandleMethodForEventType(Type, @event.Event.GetType());
        }
    }
}