// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Processing
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
    }
}