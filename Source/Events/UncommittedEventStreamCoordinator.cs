/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Coordination;
using Dolittle.Runtime.Events.Store;
using Dolittle.Time;
using Dolittle.Artifacts;
using Dolittle.Runtime.Events.Processing;
using Dolittle.Runtime.Events.Relativity;
using Dolittle.Lifecycle;
using Dolittle.DependencyInversion;

namespace Dolittle.Events.Coordination
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    /// <remarks>This implementation has been placed here temporarily.  It is not where it should be.</remarks>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        readonly FactoryFor<IEventStore> _getEventStore;
        readonly ILogger _logger;
        readonly IArtifactTypeMap _artifactMap;
        readonly ISystemClock _systemClock;
        readonly IScopedEventProcessingHub _eventProcessorHub;
        readonly IEventHorizon _eventHorizon;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="getEventStore">A <see cref="FactoryFor{IEventStore}" /> to return a correctly scoped instance of <see cref="IEventStore" /></param>
        /// <param name="eventProcessorHub"><see cref="IScopedEventProcessingHub"/> for processing in same bounded context</param>
        /// <param name="eventHorizon"><see cref="IEventHorizon"/> to pass events through to</param>
        /// <param name="artifactMap">An instance of <see cref="IArtifactTypeMap" /> to get the artifact for Event Source and Events</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for getting the time</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for accessing the current <see cref="ExecutionContext" /></param>

        public UncommittedEventStreamCoordinator(
            FactoryFor<IEventStore> getEventStore,
            IScopedEventProcessingHub eventProcessorHub,
            IEventHorizon eventHorizon,
            IArtifactTypeMap artifactMap,
            ILogger logger,
            ISystemClock systemClock,
            IExecutionContextManager executionContextManager)
            
        {
            _getEventStore = getEventStore;
            _eventProcessorHub = eventProcessorHub;
            _eventHorizon = eventHorizon;
            _logger = logger;
            _artifactMap = artifactMap;
            _systemClock = systemClock;
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc/>
        public void Commit(CorrelationId correlationId, UncommittedEvents uncommittedEvents)
        {
            _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
            _logger.Trace("Building the Event Store uncommitted event stream");
            var uncommitted = BuildUncommitted(uncommittedEvents,correlationId.Value);
            _logger.Trace("Committing the events");
            CommittedEventStream committed;
            using(var eventStore = _getEventStore())
            {
                committed = eventStore.Commit(uncommitted);
            }
            _logger.Trace("Process events in same bounded context");
            _eventProcessorHub.Process(committed);
            _logger.Trace("Passing committed events through event horizon");
            _eventHorizon.PassThrough(committed);
        }

        UncommittedEventStream BuildUncommitted(UncommittedEvents uncommittedEvents, CorrelationId correlationId)
        {
            var versionedEventSource = ToVersionedEventSource(uncommittedEvents);
            return BuildFrom(versionedEventSource,correlationId,_systemClock.GetCurrentTime(),uncommittedEvents.Events.Select(e => e.Event));

        }

        VersionedEventSource ToVersionedEventSource(UncommittedEvents uncommittedEvents)
        {
            var commit = Convert.ToUInt64(uncommittedEvents.EventSource.Version.Commit);
            var sequence = Convert.ToUInt32(uncommittedEvents.EventSource.Version.Sequence);
            return new VersionedEventSource(new EventSourceVersion(commit,sequence),uncommittedEvents.EventSource.EventSourceId, _artifactMap.GetArtifactFor(uncommittedEvents.EventSource.GetType()).Id) ;
        }

        UncommittedEventStream BuildFrom(VersionedEventSource version, CorrelationId correlationId, DateTimeOffset committed, IEnumerable<IEvent> events)
        {
            var envelopes = events.Select(e => e.ToEnvelope(BuildEventMetadata(e, EventId.New(),version, correlationId, committed))).ToList();
            if(envelopes == null || !envelopes.Any())
                throw new ApplicationException("There are no envelopes");
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