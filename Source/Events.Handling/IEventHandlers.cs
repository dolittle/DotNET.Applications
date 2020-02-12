// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that knows about <see cref="ICanHandleEvents"/>.
    /// </summary>
    public interface IEventHandlers
    {
        /// <summary>
        /// Check if there is an <see cref="ICanHandleEvents">event handler</see> for a specific <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to check for.</param>
        /// <returns>true if there is one, false if not.</returns>
        bool HasFor(EventHandlerId eventHandlerId);

        /// <summary>
        /// Get the <see cref="EventHandler"/> for a given <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to get for.</param>
        /// <returns><see cref="EventHandler"/> instance.</returns>
        EventHandler GetFor(EventHandlerId eventHandlerId);

        /// <summary>
        /// Register a <see cref="ICanHandleEvents"/> type towards a given <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to associate with.</param>
        /// <typeparam name="TEventHandler">Type of <see cref="ICanHandleEvents"/>.</typeparam>
        /// <returns><see cref="EventHandler"/> instance.</returns>
        EventHandler Register<TEventHandler>(EventHandlerId eventHandlerId = default)
            where TEventHandler : ICanHandleEvents;

        /// <summary>
        /// Register a <see cref="ICanHandleEvents"/> type towards a given <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> to associate with.</param>
        /// <returns><see cref="EventHandler"/> instance.</returns>
        EventHandler Register(Type type, EventHandlerId eventHandlerId = default);
    }
}