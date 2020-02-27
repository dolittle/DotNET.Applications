// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using grpc = contracts::Dolittle.Runtime.Events;

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
        public CommittedEvent ToSDK(grpc.CommittedEvent source)
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
            var cause = new Cause((CauseType)source.Cause.Type, source.Cause.Position);

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
    }
}