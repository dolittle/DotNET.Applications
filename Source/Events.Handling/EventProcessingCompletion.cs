// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingCompletion"/>.
    /// </summary>
    [Singleton]
    public class EventProcessingCompletion : IEventProcessingCompletion
    {
        readonly ConcurrentDictionary<Type, List<EventHandlerType>> _eventHandlersByEventType = new ConcurrentDictionary<Type, List<EventHandlerType>>();
        readonly ConcurrentDictionary<CorrelationId, EventHandlersWaiter> _eventHandlersWaitersByScope = new ConcurrentDictionary<CorrelationId, EventHandlersWaiter>();
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessingCompletion"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventProcessingCompletion(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void RegisterHandler(EventHandlerId eventHandler, IEnumerable<Type> eventTypes)
        {
            eventTypes.ForEach(_ =>
                {
                    var eventHandlerType = new EventHandlerType(eventHandler, _);
                    _eventHandlersByEventType.AddOrUpdate(_, new List<EventHandlerType> { eventHandlerType }, (k, v) =>
                        {
                            v.Add(eventHandlerType);
                            return v;
                        });
                });
        }

        /// <inheritdoc/>
        public void EventHandlerCompletedForEvent(CorrelationId correlationId, EventHandlerId eventHandlerId, Type type)
        {
            _logger.Information("Event Handler completed for Event with correlation '{CorrelationId}'", correlationId);
            if (_eventHandlersWaitersByScope.ContainsKey(correlationId))
            {
                var waiter = _eventHandlersWaitersByScope[correlationId];
                waiter.Signal(new EventHandlerType(eventHandlerId, type));
                if (waiter.IsDone())
                {
                    _eventHandlersWaitersByScope.TryRemove(correlationId, out EventHandlersWaiter _);
                }
            }
        }

        /// <inheritdoc/>
        public Task Perform(CorrelationId correlationId, IEnumerable<IEvent> events, Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            var eventTypes = events.Select(_ => _.GetType()).ToArray();
            var eventHandlersForScope = _eventHandlersByEventType
                .Where(_ => eventTypes.Contains(_.Key))
                .SelectMany(_ => _.Value);

            var waiter = new EventHandlersWaiter(correlationId, eventHandlersForScope, _logger);

            _eventHandlersWaitersByScope.AddOrUpdate(correlationId, waiter, (_, v) => v);

            Task.Run(async () =>
            {
                try
                {
                    var stopWatch = new Stopwatch();
                    action();
                    var actionTime = stopWatch.ElapsedMilliseconds;
                    _logger.Information($"EventProcessingCompletion.PERFORM LOOP Time for eventHandlers '{correlationId}': {actionTime}");

                    _logger.Information($"EventProcessingCompletion.PERFORM LOOP Waiting for EventHandlers: '{correlationId}' ");
                    stopWatch.Restart();
                    await waiter.Complete().ConfigureAwait(false);
                    stopWatch.Stop();
                    var waitingTime = stopWatch.ElapsedMilliseconds;
                    _logger.Information($"EventProcessingCompletion.PERFORM LOOP EventHandler waiting is done '{correlationId}': {waitingTime}");

                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex, "An error occurred while performing event processing completion");
                }
                finally
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}
