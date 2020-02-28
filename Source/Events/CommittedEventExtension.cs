// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events
{
    /// <summary>
    /// Extension methods for <see cref="CommittedEvent" />.
    /// </summary>
    public static class CommittedEventExtension
    {
        /// <summary>
        /// Derices the <see cref="EventContext" /> from the <see cref="CommittedEvent" />.
        /// </summary>
        /// <param name="committedEvent">The <see cref="CommittedEvent" />.</param>
        /// <returns>The derived <see cref="EventContext" />.</returns>
        public static EventContext DeriveContext(this CommittedEvent committedEvent) =>
            new EventContext(committedEvent.EventSource, committedEvent.Occurred);
    }
}