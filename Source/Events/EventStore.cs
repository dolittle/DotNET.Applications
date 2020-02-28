// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using static contracts::Dolittle.Runtime.Events.EventStore;
using grpcEvents = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore" />.
    /// </summary>
    [SingletonPerTenant]
    public class EventStore : IEventStore
    {
        readonly EventStoreClient _eventStoreClient;
        readonly IArtifactTypeMap _artifactMap;
        readonly IEventConverter _eventConverter;
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStore"/> class.
        /// </summary>
        /// <param name="eventStoreClient">The event store grpc client.</param>
        /// <param name="artifactMap">The <see cref="IArtifactTypeMap" />.</param>
        /// <param name="eventConverter">The <see cref="IEventConverter" />.</param>
        /// <param name="serializer">The <see cref="ISerializer" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventStore(EventStoreClient eventStoreClient, IArtifactTypeMap artifactMap, IEventConverter eventConverter, ISerializer serializer, ILogger logger)
        {
            _artifactMap = artifactMap;
            _eventStoreClient = eventStoreClient;
            _eventConverter = eventConverter;
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public CommittedEvents Commit(UncommittedEvents uncommittedEvents)
        {
            var request = _eventConverter.ToProtobuf(uncommittedEvents);
            var response = _eventStoreClient.Commit(request);
            ThrowIfUnsuccessfullRespone(response.Success, response.Reason);
            return CommittedEventsFromResponse(response.Events);
        }

        /// <inheritdoc/>
        public CommittedAggregateEvents CommitForAggregate(UncommittedAggregateEvents uncommittedAggregateEvents)
        {
            var request = _eventConverter.ToProtobuf(uncommittedAggregateEvents);
            var response = _eventStoreClient.CommitForAggregate(request);
            ThrowIfUnsuccessfullRespone(response.Success, response.Reason);
            return CommittedEventsFromResponse(response.Events);
        }

        /// <inheritdoc/>
        public CommittedAggregateEvents FetchForAggregate(EventSourceId eventSourceId, ArtifactId aggregateRoot)
        {
            var request = new grpcEvents.Aggregate { EventSource = eventSourceId.ToProtobuf(), AggregateRoot = aggregateRoot.ToProtobuf() };
            var response = _eventStoreClient.FetchForAggregate(request);
            ThrowIfUnsuccessfullRespone(response.Success, response.Reason);
            return CommittedEventsFromResponse(response.Events);
        }

        CommittedEvents CommittedEventsFromResponse(grpcEvents.CommittedEvents committedEvents) =>
            new CommittedEvents(
                committedEvents.Events
                    .Select(_eventConverter.ToSDK)
                    .ToArray());

        CommittedAggregateEvents CommittedEventsFromResponse(grpcEvents.CommittedAggregateEvents committedEvents)
        {
            var aggregateRoot = _artifactMap.GetTypeFor(new Artifact(committedEvents.AggregateRoot.ToGuid(), ArtifactGeneration.First));
            var aggregateRootVersion = committedEvents.AggregateRootVersion;
            return new CommittedAggregateEvents(
                committedEvents.EventSource.ToGuid(),
                aggregateRoot,
                aggregateRootVersion,
                committedEvents.Events
                    .Select(_eventConverter.ToSDK)
                    .Select(_ => new CommittedAggregateEvent(
                        _.EventSource,
                        aggregateRoot,
                        aggregateRootVersion,
                        _.EventLogVersion,
                        _.Occurred,
                        _.CorrelationId,
                        _.Microservice,
                        _.Tenant,
                        _.Cause,
                        _.Event))
                    .ToArray());
        }

        void ThrowIfUnsuccessfullRespone(bool success, string reason)
        {
            if (!success) throw new EventStoreOperationFailed(reason);
        }
    }
}