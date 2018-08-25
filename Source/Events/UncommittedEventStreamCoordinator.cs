/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Execution;
using Dolittle.Runtime.Transactions;
using Dolittle.Logging;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Coordination;
using Dolittle.Runtime.Events.Store;
using Dolittle.Events;
using Dolittle.Time;
using Dolittle.Artifacts;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Relativity;

namespace Dolittle.Events.Coordination
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    /// <remarks>This implementation has been placed here temporarily.  It is not where it should be.</remarks>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        readonly IEventStore _eventStore;
        readonly ILogger _logger;
        readonly IArtifactTypeMap _artifactMap;
        readonly ISystemClock _systemClock;
        readonly IEventProcessors _eventProcessors;
        readonly IEventHorizon _eventHorizon;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore">An instance of <see cref="IEventStore" /> to persist events</param>
        /// <param name="eventProcessors"><see cref="IEventProcessors"/> for processing in same bounded context</param>
        /// <param name="eventHorizon"><see cref="IEventHorizon"/> to pass events through to</param>
        /// <param name="artifactMap">An instance of <see cref="IArtifactTypeMap" /> to get the artifact for Event Source and Events</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for getting the time</param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            IEventProcessors eventProcessors,
            IEventHorizon eventHorizon,
            IArtifactTypeMap artifactMap,
            ILogger logger,
            ISystemClock systemClock)
            
        {
            _eventStore = eventStore;
            _eventProcessors = eventProcessors;
            _eventHorizon = eventHorizon;
            _logger = logger;
            _artifactMap = artifactMap;
            _systemClock = systemClock;
        }

        /// <inheritdoc/>
        public void Commit(TransactionCorrelationId correlationId, Dolittle.Runtime.Events.UncommittedEventStream uncommittedEventStream)
        {
            _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
            _logger.Trace("Building the Event Store uncommitted event stream");
            var uncommitted = BuildUncommitted(uncommittedEventStream,correlationId.Value);
            _logger.Trace("Committing the events");
            var committed = _eventStore.Commit(uncommitted);
            _logger.Trace("Process events in same bounded context");
            _eventProcessors.Process(committed);
            _logger.Trace("Passing committed events through event horizon");
            _eventHorizon.PassThrough(committed);
        }

        Dolittle.Runtime.Events.Store.UncommittedEventStream BuildUncommitted(Dolittle.Runtime.Events.UncommittedEventStream uncommittedEventStream, CorrelationId correlationId)
        {
            var versionedEventSource = ToVersionedEventSource(uncommittedEventStream);
            return BuildFrom(versionedEventSource,correlationId,_systemClock.GetCurrentTime(),uncommittedEventStream.EventsAndVersion.Select(e => e.Event));

        }

        VersionedEventSource ToVersionedEventSource(Dolittle.Runtime.Events.UncommittedEventStream uncommittedEventStream)
        {
            var commit = Convert.ToUInt64(uncommittedEventStream.EventSource.Version.Commit);
            var sequence = Convert.ToUInt32(uncommittedEventStream.EventSource.Version.Sequence);
            return new VersionedEventSource(new Dolittle.Runtime.Events.Store.EventSourceVersion(commit,sequence),uncommittedEventStream.EventSource.EventSourceId, _artifactMap.GetArtifactFor(uncommittedEventStream.EventSource.GetType()).Id) ;
        }

        Dolittle.Runtime.Events.Store.UncommittedEventStream BuildFrom(VersionedEventSource version, CorrelationId correlationId, DateTimeOffset committed, IEnumerable<IEvent> events)
        {
            var envelopes = events.Select(e => e.ToEnvelope(EventId.New(),BuildEventMetadata(e, version, correlationId, committed))).ToList();
            if(envelopes == null || !envelopes.Any())
                throw new ApplicationException("There are no envelopes");
            return BuildStreamFrom(EventStream.From(envelopes));
        }

        EventMetadata BuildEventMetadata(IEvent @event, VersionedEventSource versionedEventSource, CorrelationId correlationId, DateTimeOffset committed)
        {
            return new EventMetadata(versionedEventSource, correlationId, _artifactMap.GetArtifactFor(@event.GetType()), "A Test", committed);
        }

        Dolittle.Runtime.Events.Store.UncommittedEventStream BuildStreamFrom(EventStream stream)
        {
            var now = DateTimeOffset.UtcNow;
            var lastEvent = stream.Last();
            var versionedEventSource = lastEvent.Metadata.VersionedEventSource;
            var correlationId = lastEvent.Metadata.CorrelationId;
            return new Dolittle.Runtime.Events.Store.UncommittedEventStream(CommitId.New(), correlationId, versionedEventSource, now, stream);
        }
    }
}