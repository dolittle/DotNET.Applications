// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq;
using Dolittle.ApplicationModel;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;
using grpcEvents = contracts::Dolittle.Runtime.Events;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventConverter"/>.
    /// </summary>
    public class EventConverter : IEventConverter
    {
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventConverter"/> class.
        /// </summary>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="serializer"><see cref="ISerializer"/> for serialization.</param>
        public EventConverter(
            IArtifactTypeMap artifactTypeMap,
            ISerializer serializer)
        {
            _artifactTypeMap = artifactTypeMap;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public grpcEvents.UncommittedEvent ToProtobuf(IEvent @event)
        {
            var artifact = _artifactTypeMap.GetArtifactFor(@event.GetType());
            return new grpcEvents.UncommittedEvent
                {
                    Artifact = new grpcArtifacts.Artifact
                    {
                        Id = artifact.Id.ToProtobuf(),
                        Generation = artifact.Generation
                    },
                    Public = typeof(IPublicEvent).IsAssignableFrom(@event.GetType()),
                    Content = _serializer.EventToJson(@event)
                };
        }

        /// <inheritdoc/>
        public grpcEvents.UncommittedEvents ToProtobuf(UncommittedEvents uncommittedEvents)
        {
            var protobuf = new grpcEvents.UncommittedEvents();
            protobuf.Events.AddRange(uncommittedEvents.Select(ToProtobuf));
            return protobuf;
        }

        /// <inheritdoc/>
        public grpcEvents.UncommittedAggregateEvents ToProtobuf(UncommittedAggregateEvents uncommittedEvents)
        {
            var protobuf = new grpcEvents.UncommittedAggregateEvents
            {
                AggregateRoot = _artifactTypeMap.GetArtifactFor(uncommittedEvents.AggregateRoot).Id.ToProtobuf(),
                EventSource = uncommittedEvents.EventSource.ToProtobuf(),
                ExpectedAggregateRootVersion = uncommittedEvents.ExpectedAggregateRootVersion
            };

            protobuf.Events.AddRange(uncommittedEvents.Select(ToProtobuf));
            return protobuf;
        }

        /// <inheritdoc/>
        public CommittedEvent ToSDK(grpcEvents.CommittedEvent source)
        {
            var artifactId = source.Type.Id.To<ArtifactId>();
            var artifact = new Artifact(artifactId, source.Type.Generation);
            var type = _artifactTypeMap.GetTypeFor(artifact);
            var eventInstance = _serializer.JsonToEvent(type, source.Content) as IEvent;
            var occurred = source.Occurred.ToDateTimeOffset();
            var eventSource = source.EventSource.To<EventSourceId>();
            var correlationId = source.Correlation.To<CorrelationId>();
            var microservice = source.Microservice.To<Microservice>();
            var tenantId = source.Tenant.To<TenantId>();
            var cause = new Cause((CauseType)source.Cause.Type, source.Cause.SequenceNumber);

            return new CommittedEvent(
                source.EventLogSequenceNumber,
                occurred,
                eventSource,
                correlationId,
                microservice,
                tenantId,
                cause,
                eventInstance);
        }

        /// <inheritdoc/>
        public CommittedAggregateEvent ToSDK(grpcEvents.CommittedAggregateEvent source, EventSourceId eventSource, Type aggregateRootType)
        {
            var artifactId = source.Type.Id.To<ArtifactId>();
            var artifact = new Artifact(artifactId, source.Type.Generation);
            var type = _artifactTypeMap.GetTypeFor(artifact);
            var eventInstance = _serializer.JsonToEvent(type, source.Content) as IEvent;
            var occurred = source.Occurred.ToDateTimeOffset();
            var correlationId = source.Correlation.To<CorrelationId>();
            var microservice = source.Microservice.To<Microservice>();
            var tenantId = source.Tenant.To<TenantId>();
            var cause = new Cause((CauseType)source.Cause.Type, source.Cause.SequenceNumber);

            return new CommittedAggregateEvent(
                eventSource,
                aggregateRootType,
                source.AggregateRootVersion,
                source.EventLogSequenceNumber,
                occurred,
                correlationId,
                microservice,
                tenantId,
                cause,
                eventInstance);
        }
    }
}