// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents a waiter for event handlers.
    /// </summary>
    public class EventHandlersWaiter
    {
        readonly object _lockObject = new object();
        readonly ConcurrentDictionary<Type, int> _eventTypeAndCounts;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlersWaiter"/> class.
        /// </summary>
        /// <param name="eventTypeAndCounts"><see cref="IDictionary{TKey, TValue}"/> with count references.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlersWaiter(IDictionary<Type, int> eventTypeAndCounts, ILogger logger)
        {
            _eventTypeAndCounts = new ConcurrentDictionary<Type, int>(eventTypeAndCounts);
            _logger = logger;
        }

        /// <summary>
        /// Signal for a specific event type.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of event to signel for.</param>
        public void Signal(Type type)
        {
            lock (_lockObject)
            {
                _eventTypeAndCounts[type]--;
                if (_eventTypeAndCounts[type] < 0) _eventTypeAndCounts[type] = 0;
            }
        }

        /// <summary>
        /// Check whether or not we're done or not.
        /// </summary>
        /// <returns>true if we're done, false if not.</returns>
        public bool IsDone()
        {
            lock (_lockObject)
            {
                return _eventTypeAndCounts.Values.All(_ => _ == 0);
            }
        }

        /// <summary>
        /// Wait for all event handlers to be done.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Complete()
        {
            const int delay = 20;

            var tcs = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                var timeout = 30000 / delay;

                while (!IsDone())
                {
                    Task.Delay(delay).Wait();
                    if (timeout-- == 0)
                    {
                        _logger.Trace("Waiting for event handlers timed out");
                        break;
                    }
                }

                tcs.SetResult(true);
            });

            return tcs.Task;
        }
    }
}