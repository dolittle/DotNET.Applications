// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="EventHandlerId" /> is an illegal value.
    /// </summary>
    public class IllegalEventHandlerId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalEventHandlerId"/> class.
        /// </summary>
        /// <param name="eventHandlerId">The <see cref="EventHandlerId" />.</param>
        public IllegalEventHandlerId(EventHandlerId eventHandlerId)
            : base($"Event handler id cannot be '{eventHandlerId}'")
        {
        }
    }
}