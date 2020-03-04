// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that can process events for a stream.
    /// </summary>
    public interface ICanProcessEventsInStream
    {
        /// <summary>
        /// Gets the <see cref="StreamId"/> for the stream.
        /// </summary>
        StreamId StreamId { get; }

        /// <summary>
        /// Process a <see cref="CommittedEvent"/>.
        /// </summary>
        /// <param name="partition"><see cref="PartitionId"/> the <see cref="CommittedEvent"/> belongs to.</param>
        /// <param name="event"><see cref="CommittedEvent"/> to process.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Process(PartitionId partition, CommittedEvent @event);
    }
}