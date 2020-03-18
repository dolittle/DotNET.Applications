// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an event handler has already been registered with a given <see cref="EventHandlerId" />.
    /// </summary>
    public class EventHandlerAlreadyRegistered : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAlreadyRegistered"/> class.
        /// </summary>
        /// <param name="eventHandlerId">The <see cref="EventHandlerId" />.</param>
        public EventHandlerAlreadyRegistered(EventHandlerId eventHandlerId)
            : base($"An event handler with id '{eventHandlerId}' has already been registered")
        {
        }
    }
}