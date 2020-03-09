// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlersWaiters"/>.
    /// </summary>
    [Singleton]
    public class EventHandlersWaiters : IEventHandlersWaiters
    {
        readonly ConcurrentDictionary<EventHandlerId, IEnumerable<Type>> _eventTypesByEventHandler = new ConcurrentDictionary<EventHandlerId, IEnumerable<Type>>();
        readonly ConcurrentDictionary<Type, List<EventHandlersWaiter>> _eventHandlersWaiterByEventType = new ConcurrentDictionary<Type, List<EventHandlersWaiter>>();
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlersWaiters"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlersWaiters(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void RegisterHandler(EventHandler eventHandler)
        {
            _eventTypesByEventHandler[eventHandler.Identifier] = eventHandler.EventTypes;
        }

        /// <inheritdoc/>
        public void SignalDone(CorrelationId correlationId, EventHandlerId eventHandlerId, Type type)
        {
            if (_eventHandlersWaiterByEventType.ContainsKey(type))
            {
                var waiters = _eventHandlersWaiterByEventType[type];
                foreach (var waiter in waiters)
                {
                    waiter.Signal(type);
                }

                _eventHandlersWaiterByEventType[type] = waiters.Where(_ => !_.IsDone()).ToList();
            }
        }

        /// <inheritdoc/>
        public EventHandlersWaiter GetWaiterFor(CorrelationId correlationId, params Type[] types)
        {
            var typeCounts = types.ToDictionary(_ => _, _ => 0);
            _eventTypesByEventHandler.ForEach(_ => _.Value.ForEach(et => typeCounts[et]++));
            var waiter = new EventHandlersWaiter(typeCounts, _logger);

            foreach (var type in types)
            {
                var waiters = _eventHandlersWaiterByEventType[type] =
                                _eventHandlersWaiterByEventType.ContainsKey(type) ?
                                _eventHandlersWaiterByEventType[type] :
                                new List<EventHandlersWaiter>();

                waiters.Add(waiter);
            }

            return waiter;
        }
    }
}