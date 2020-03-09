// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Execution;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines an interface for waiting for all event handlers to be done.
    /// </summary>
    public interface IEventHandlersWaiters
    {
        /// <summary>
        /// Register an <see cref="EventHandler"/> for waiting when appropriate.
        /// </summary>
        /// <param name="eventHandler"><see cref="EventHandler"/> to register.</param>
        void RegisterHandler(EventHandler eventHandler);

        /// <summary>
        /// Signal that a specific event handler with a given Id is done handling a specific <see cref="IEvent"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="CorrelationId"/> in which the event is handled in context of.</param>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> that was done handling.</param>
        /// <param name="eventType"><see cref="Type"/> of event that was handled.</param>
        void SignalDone(CorrelationId correlationId, EventHandlerId eventHandlerId, Type eventType);

        /// <summary>
        /// Wait for a specific <see cref="CorrelationId"/> to be done with all handlers that should be done.
        /// </summary>
        /// <param name="correlationId"><see cref="CorrelationId"/> to wait for.</param>
        /// <param name="eventTypes">All the types of <see cref="IEvent">events</see> to wait for.</param>
        /// <returns>Task to wait on.</returns>
        EventHandlersWaiter GetWaiterFor(CorrelationId correlationId, params Type[] eventTypes);
    }
}