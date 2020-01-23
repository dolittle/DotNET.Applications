// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Coordination;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Relativity;
using Dolittle.Runtime.Events.Store;
using Dolittle.Serialization.Json;
using Dolittle.Time;
using Google.Protobuf.WellKnownTypes;
using static Dolittle.Events.Runtime.EventStore;
using grpc = Dolittle.Events.Runtime;

namespace Dolittle.Events.Coordination
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>.
    /// </summary>
    /// <remarks>This implementation has been placed here temporarily.  It is not where it should be.</remarks>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        readonly EventStoreClient _eventStoreClient;
        readonly ILogger _logger;
        readonly IArtifactTypeMap _artifactMap;
        readonly ISystemClock _systemClock;
        readonly IScopedEventProcessingHub _eventProcessorHub;
        readonly IEventHorizon _eventHorizon;
        readonly IExecutionContextManager _executionContextManager;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UncommittedEventStreamCoordinator"/> class.
        /// </summary>
        /// <param name="eventStoreClient">A see cref="EventStoreClient"/> for connecting to the runtime.</param>
        /// <param name="eventProcessorHub"><see cref="IScopedEventProcessingHub"/> for processing in same bounded context.</param>
        /// <param name="eventHorizon"><see cref="IEventHorizon"/> to pass events through to.</param>
        /// <param name="artifactMap">An instance of <see cref="IArtifactTypeMap" /> to get the artifact for Event Source and Events.</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging.</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for getting the time.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for accessing the current <see cref="ExecutionContext" />.</param>
        /// <param name="serializer">A JSON <see cref="ISerializer"/>.</param>
        public UncommittedEventStreamCoordinator(
            EventStoreClient eventStoreClient,
            IScopedEventProcessingHub eventProcessorHub,
            IEventHorizon eventHorizon,
            IArtifactTypeMap artifactMap,
            ILogger logger,
            ISystemClock systemClock,
            IExecutionContextManager executionContextManager,
            ISerializer serializer)
        {
            _eventStoreClient = eventStoreClient;
            _eventProcessorHub = eventProcessorHub;
            _eventHorizon = eventHorizon;
            _logger = logger;
            _artifactMap = artifactMap;
            _systemClock = systemClock;
            _executionContextManager = executionContextManager;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public void Commit(CorrelationId correlationId, UncommittedEvents uncommittedEvents)
        {
            _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
            _logger.Trace("Building the Event Store uncommitted event stream");
            var uncommitted = BuildUncommitted(uncommittedEvents, correlationId.Value);
            _logger.Trace("Committing the events");

            var uncommittedEventsToSend = new grpc.UncommittedEvents();
            uncommittedEvents.ForEach(_ =>
            {
                var artifact = _artifactMap.GetArtifactFor(_.GetType());
                var serialized = _serializer.ToJson(_, SerializationOptions.CamelCase);
                var uncommittedEvent = new grpc.UncommittedEvent
                {
                    Occurred = Timestamp.FromDateTimeOffset(uncommitted.Timestamp),
                    Artifact = new grpc.Artifact
                    {
                        Id = artifact.Id.ToProtobuf(),
                        Generation = artifact.Generation
                    },
                    Data = serialized
                };
                uncommittedEventsToSend.Events.Add(uncommittedEvent);
            });

            _eventStoreClient.Commit(uncommittedEventsToSend);

            var committed = new CommittedEventStream(0, uncommitted.Source, uncommitted.Id, uncommitted.CorrelationId, uncommitted.Timestamp, uncommitted.Events);
            try
            {
                _eventProcessorHub.Process(committed);
                _logger.Trace("Process events in same bounded context");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error processing CommittedEventStream within local event processors '{committed?.Sequence?.ToString() ?? "[NULL]"}'");
            }
#if false
            try
            {
                _logger.Trace("Passing committed events through event horizon");
                _eventHorizon.PassThrough(new CommittedEventStreamWithContext(committed, _executionContextManager.Current));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error processing CommittedEventStream within event horizons '{committed?.Sequence?.ToString() ?? "[NULL]"}'");
            }
#endif
        }

        UncommittedEventStream BuildUncommitted(UncommittedEvents uncommittedEvents, CorrelationId correlationId)
        {
            var versionedEventSource = ToVersionedEventSource(uncommittedEvents);
            return BuildFrom(versionedEventSource, correlationId, _systemClock.GetCurrentTime(), uncommittedEvents.Events);
        }

        VersionedEventSource ToVersionedEventSource(UncommittedEvents uncommittedEvents)
        {
            var commit = Convert.ToUInt64(uncommittedEvents.EventSource.Version.Commit);
            var sequence = Convert.ToUInt32(uncommittedEvents.EventSource.Version.Sequence);
            var key = new EventSourceKey(uncommittedEvents.EventSource.EventSourceId, _artifactMap.GetArtifactFor(uncommittedEvents.EventSource.GetType()).Id);
            return new VersionedEventSource(new EventSourceVersion(commit, sequence), key);
        }

        VersionedEventSource ToVersionedEventSource(VersionedEvent versionedEvent, EventSourceId eventSourceId, ArtifactId artifact)
        {
            return new VersionedEventSource(new EventSourceVersion(versionedEvent.Version.Commit, versionedEvent.Version.Sequence), new EventSourceKey(eventSourceId, artifact));
        }

        UncommittedEventStream BuildFrom(VersionedEventSource version, CorrelationId correlationId, DateTimeOffset committed, IEnumerable<VersionedEvent> events)
        {
            var envelopes = events.Select(e => e.Event.ToEnvelope(BuildEventMetadata(e.Event, EventId.New(), ToVersionedEventSource(e, version.EventSource, version.Artifact), correlationId, committed))).ToList();
            if (envelopes?.Any() != true)
                throw new MissingEnvelopes();

            return BuildStreamFrom(EventStream.From(envelopes));
        }

        EventMetadata BuildEventMetadata(IEvent @event, EventId id, VersionedEventSource versionedEventSource, CorrelationId correlationId, DateTimeOffset committed)
        {
            return new EventMetadata(id, versionedEventSource, correlationId, _artifactMap.GetArtifactFor(@event.GetType()), committed, _executionContextManager.Current);
        }

        UncommittedEventStream BuildStreamFrom(EventStream stream)
        {
            var now = DateTimeOffset.UtcNow;
            var lastEvent = stream.Last();
            var versionedEventSource = lastEvent.Metadata.VersionedEventSource;
            var correlationId = lastEvent.Metadata.CorrelationId;
            return new UncommittedEventStream(CommitId.New(), correlationId, versionedEventSource, now, stream);
        }
    }
}