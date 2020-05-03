// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling.Internal
{
    /// <summary>
    /// Exception that gets thrown when trying to invoke an event handler with an event of incorrect type.
    /// </summary>
    public class EventTypeIsIncorrectForEventHandler : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeIsIncorrectForEventHandler"/> class.
        /// </summary>
        /// <param name="expectedType">The <see cref="Type"/> that the event handler accepts.</param>
        /// <param name="eventType">The <see cref="Type"/> of the event to handle.</param>
        public EventTypeIsIncorrectForEventHandler(Type expectedType, Type eventType)
            : base($"The event handler expects events of type {expectedType}, but was provided an event of type {eventType}")
        {
        }
    }
}