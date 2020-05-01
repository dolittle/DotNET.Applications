// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that can filter instances of <see cref="IEvent"/> to a partitioned stream.
    /// </summary>
    public interface ICanFilterEventsWithPartition
    {
        /// <summary>
        /// Method that will be called to determine whether an <see cref="IEvent"/> should be part of the partitioned stream or not.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> to filter.</param>
        /// <param name="context">The <see cref="EventContext"/> of the event to filter.</param>
        /// <returns>A <see cref="Task{T}"/> of type <see cref="PartitonedFilterResult"/> representing the result of the asynchronous operation.</returns>
        Task<PartitonedFilterResult> Filter(IEvent @event, EventContext context);
    }
}