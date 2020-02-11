// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a system that knows about <see cref="ICanHandleEvents"/> and provides a
    /// <see cref="ICanFilterEventsInStream">filter</see> and <see cref="ICanProcessEventsInStream">processor</see>.
    /// </summary>
    public class EventHandlerFilterAndProcessor : ICanFilterEventsInStream, ICanProcessEventsInStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerFilterAndProcessor"/> class.
        /// </summary>
        /// <param name="eventHandlerId"><see cref="EventHandlerId"/> for the <see cref="ICanHandleEvents"/>.</param>
        public EventHandlerFilterAndProcessor(EventHandlerId eventHandlerId)
        {
            StreamId = StreamId.AllStream;
            FilterId = eventHandlerId.Value;
        }

        /// <inheritdoc/>
        public StreamId StreamId { get; }

        /// <inheritdoc/>
        public FilterId FilterId { get; }

        /// <inheritdoc/>
        public async Task<FilterResult> Filter(CommittedEvent @event)
        {
            await Task.CompletedTask.ConfigureAwait(false);
            return false;
        }

        /// <inheritdoc/>
        public async Task Process(PartitionId partition, CommittedEvent @event)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}