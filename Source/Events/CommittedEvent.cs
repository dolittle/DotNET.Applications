// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents a committed event.
    /// </summary>
    public class CommittedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommittedEvent"/> class.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> instance.</param>
        /// <param name="occurred">The <see cref="DateTimeOffset"/> for when it occurred.</param>
        public CommittedEvent(
            IEvent @event,
            DateTimeOffset occurred)
        {
            Event = @event;
            Occurred = occurred;
        }

        /// <summary>
        /// Gets the <see cref="IEvent"/>.
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// Gets <see cref="DateTimeOffset">when</see> it occurred.
        /// </summary>
        public DateTimeOffset Occurred { get; }
    }
}