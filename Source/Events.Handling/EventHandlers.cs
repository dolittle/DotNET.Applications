// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlers"/>.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        readonly ConcurrentDictionary<EventHandlerId, AbstractEventHandler> _eventHandlers = new ConcurrentDictionary<EventHandlerId, AbstractEventHandler>();
        readonly IEventHandlerProcessor _eventHandlerProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="eventHandlerProcessor">The <see cref="IEventHandlerProcessor" />.</param>
        public EventHandlers(IEventHandlerProcessor eventHandlerProcessor)
        {
            _eventHandlerProcessor = eventHandlerProcessor;
        }

        /// <inheritdoc/>
        public AbstractEventHandler GetFor(EventHandlerId eventHandlerId)
        {
            ThrowIfMissingEventHandlerWithId(eventHandlerId);
            return _eventHandlers[eventHandlerId];
        }

        /// <inheritdoc/>
        public bool HasFor(EventHandlerId eventHandlerId) => _eventHandlers.ContainsKey(eventHandlerId);

        /// <inheritdoc/>
        public void Register(AbstractEventHandler eventHandler)
        {
            if (!_eventHandlers.TryAdd(eventHandler.Identifier, eventHandler)) throw new EventHandlerAlreadyRegistered(eventHandler.Identifier);
        }

        /// <inheritdoc/>
        public void DeRegister(EventHandlerId eventHandler) => _eventHandlers.Remove(eventHandler, out var _);

        /// <inheritdoc/>
        public Task Start(AbstractEventHandler eventHandler, CancellationToken cancellation) => _eventHandlerProcessor.Start(eventHandler, cancellation);

        void ThrowIfMissingEventHandlerWithId(EventHandlerId eventHandlerId)
        {
            if (!HasFor(eventHandlerId))
            {
                throw new MissingEventHandlerWithId(eventHandlerId);
            }
        }
    }
}