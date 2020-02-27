// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events
{
    /// <summary>
    /// Reoresents the context in which an event occured in.
    /// </summary>
    public class EventContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventContext"/> class.
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> that the event is coming from.</param>
        /// <param name="occurred">The <see cref="DateTimeOffset" /> when the Event was committed to the Event Store.</param>
        public EventContext(
            EventSourceId eventSourceId,
            DateTimeOffset occurred)
        {
            EventSourceId = eventSourceId;
            Occurred = occurred;
        }

        /// <summary>
        /// Gets the <see cref="EventSourceId"/> that the event is coming from.
        /// </summary>
        public EventSourceId EventSourceId { get; }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset" /> when the Event was committed to the Event Store.
        /// </summary>
        public DateTimeOffset Occurred { get; }
    }
}