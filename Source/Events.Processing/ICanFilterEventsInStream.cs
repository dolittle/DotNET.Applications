// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that can filter events for a stream.
    /// </summary>
    public interface ICanFilterEventsInStream
    {
        /// <summary>
        /// Gets the source <see cref="StreamId"/>.
        /// </summary>
        StreamId StreamId { get; }

        /// <summary>
        /// Gets the <see cref="FilterId"/>.
        /// </summary>
        FilterId FilterId { get; }

        /// <summary>
        /// Method that is asked if event is accepted.
        /// </summary>
        /// <param name="event"><see cref="CommittedEvent"/> to ask if is accepted.</param>
        /// <returns>true if accepted, false if not.</returns>
        Task<FilterResult> Filter(CommittedEvent @event);
    }
}