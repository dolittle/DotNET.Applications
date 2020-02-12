// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an event handler with a specific <see cref="EventHandlerId"/> is not registered in the system.
    /// </summary>
    public class MissingEventHandlerWithId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingEventHandlerWithId"/> class.
        /// </summary>
        /// <param name="eventHandlerId">The <see cref="EventHandlerId">identifier</see>.</param>
        public MissingEventHandlerWithId(EventHandlerId eventHandlerId)
            : base($"Event handler with {eventHandlerId} is not registered")
        {
        }
    }
}