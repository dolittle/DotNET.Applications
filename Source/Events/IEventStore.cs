// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;

namespace Dolittle.Events
{
    /// <summary>
    /// Defines an interface for working directly with the Event Store.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Commits <see cref="UncommittedEvents" />.
        /// </summary>
        /// <param name="uncommittedEvents">The <see cref="UncommittedEvents" />.</param>
        /// <returns>The <see cref="CommittedEvents" />.</returns>
        CommittedEvents Commit(UncommittedEvents uncommittedEvents);

        /// <summary>
        /// Commits <see cref="UncommittedAggregateEvents" />.
        /// </summary>
        /// <param name="uncommittedAggregateEvents">The <see cref="UncommittedAggregateEvents" />.</param>
        /// <returns>The <see cref="CommittedAggregateEvents" />.</returns>
        CommittedAggregateEvents CommitForAggregate(UncommittedAggregateEvents uncommittedAggregateEvents);

        /// <summary>
        /// Fetch <see cref="CommittedAggregateEvents" /> for a <see cref="AggregateRoot" />.
        /// </summary>
        /// <param name="aggregateEventSourceId">The <see cref="EventSourceId" /> of the Aggregate.</param>
        /// <returns>The <see cref="CommittedAggregateEvents" /> on from this Aggregate.</returns>
        CommittedAggregateEvents FetchForAggregate(EventSourceId aggregateEventSourceId);
    }
}