// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that knows about event handlers.
    /// </summary>
    public interface IEventHandlers
    {
        /// <summary>
        /// Check if there is an event handler for a specific <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to check for.</param>
        /// <returns>true if there is one, false if not.</returns>
        bool HasFor(EventHandlerId eventHandlerId);

        /// <summary>
        /// Get the <see cref="AbstractEventHandler"/> for a given <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to get for.</param>
        /// <returns><see cref="AbstractEventHandler"/> instance.</returns>
        AbstractEventHandler GetFor(EventHandlerId eventHandlerId);

        /// <summary>
        /// Register an event handler.
        /// </summary>
        /// <param name="eventHandler">The <see cref="AbstractEventHandler"/>.</param>
        void Register(AbstractEventHandler eventHandler);

        /// <summary>
        /// De-registers an event handler.
        /// </summary>
        /// <param name="eventHandler">The <see cref="EventHandlerId"/>.</param>
        void DeRegister(EventHandlerId eventHandler);

        /// <summary>
        /// Starts processing an event handler.
        /// </summary>
        /// <param name="abstractEventHandler"><see cref="AbstractEventHandler" />.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Start(AbstractEventHandler abstractEventHandler, CancellationToken token = default);
    }
}