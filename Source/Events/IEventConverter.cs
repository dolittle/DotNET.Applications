// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using grpc = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events
{
    /// <summary>
    /// Defines a system that is capable of converting events to and from protobuf.
    /// </summary>
    public interface IEventConverter
    {
        /// <summary>
        /// Convert from <see cref="grpc.CommittedEvent"/> to <see cref="global::Dolittle.Events.CommittedEvent"/>.
        /// </summary>
        /// <param name="source"><see cref="grpc.CommittedEvent"/>.</param>
        /// <returns>Converted <see cref="global::Dolittle.Events.CommittedEvent"/>.</returns>
        CommittedEvent ToSDK(grpc.CommittedEvent source);

        /// <summary>
        /// Convert from <see cref="grpc.CommittedAggregateEvent"/> to <see cref="global::Dolittle.Events.CommittedAggregateEvent"/>.
        /// </summary>
        /// <param name="source"><see cref="grpc.CommittedAggregateEvent"/>.</param>
        /// <param name="eventSource">The <see cref="EventSourceId" />.</param>
        /// <param name="aggregateRootType">The aggregate root <see cref="Type" />.</param>
        /// <returns>Converted <see cref="global::Dolittle.Events.CommittedAggregateEvent"/>.</returns>
        CommittedAggregateEvent ToSDK(grpc.CommittedAggregateEvent source, EventSourceId eventSource, Type aggregateRootType);

        /// <summary>
        /// Convert from <see cref="IEvent" /> to <see cref="grpc.UncommittedEvent" />.
        /// </summary>
        /// <param name="event"><see cref="IEvent" />.</param>
        /// <returns>Converted <see cref="grpc.UncommittedEvent" />.</returns>
        grpc.UncommittedEvent ToProtobuf(IEvent @event);

        /// <summary>
        /// Convert from <see cref="UncommittedEvents" /> to <see cref="grpc.UncommittedEvents" />.
        /// </summary>
        /// <param name="uncommittedEvents"><see cref="UncommittedEvents" />.</param>
        /// <returns>Converted <see cref="grpc.UncommittedEvents" />.</returns>
        grpc.UncommittedEvents ToProtobuf(UncommittedEvents uncommittedEvents);

        /// <summary>
        /// Convert from <see cref="UncommittedAggregateEvents" /> to <see cref="grpc.UncommittedAggregateEvents" />.
        /// </summary>
        /// <param name="uncommittedEvents"><see cref="UncommittedAggregateEvents" />.</param>
        /// <returns>Converted <see cref="grpc.UncommittedAggregateEvents" />.</returns>
        grpc.UncommittedAggregateEvents ToProtobuf(UncommittedAggregateEvents uncommittedEvents);
    }
}