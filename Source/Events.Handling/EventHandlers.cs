// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlers"/>.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        readonly ConcurrentDictionary<EventHandlerId, AbstractEventHandler> _eventHandlers = new ConcurrentDictionary<EventHandlerId, AbstractEventHandler>();
        readonly IEventHandlerProcessors _eventHandlerProcessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="eventHandlerProcessors">The <see cref="IEventHandlerProcessors" />.</param>
        public EventHandlers(IEventHandlerProcessors eventHandlerProcessors)
        {
            _eventHandlerProcessors = eventHandlerProcessors;
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
            _eventHandlerProcessors.Start(eventHandler);
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