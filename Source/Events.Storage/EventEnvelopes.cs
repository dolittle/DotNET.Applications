/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Runtime.Execution;
using Dolittle.Time;
using Dolittle.Runtime.Transactions;
using Dolittle.Runtime.Events.Migration;
using Dolittle.Events;
using Dolittle.Runtime.Events.Storage;
using Dolittle.Runtime.Events;

namespace Dolittle.Events.Storage
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventEnvelopes"/>
    /// </summary>
    public class EventEnvelopes : IEventEnvelopes
    {
        IApplicationArtifactIdentifierToTypeMaps _aaiToTypeMaps;
        ISystemClock _systemClock;
        IExecutionContext _executionContext;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        /// <summary>
        /// Initializes a new instance of <see cref="EventEnvelopes"/>
        /// </summary>
        /// <param name="aaiToTypeMaps"><see cref="IApplicationArtifactIdentifierToTypeMaps"/> for identifying artifacts</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for working with time</param>
        /// <param name="executionContext"><see cref="IExecutionContext"/> for working with metadata related to current execution context</param>
        /// <param name="eventMigrationHierarchyManager"><see cref="IEventMigrationHierarchyManager"/> for working with the migration levels of <see cref="IEvent">events</see></param>
        public EventEnvelopes(
            IApplicationArtifactIdentifierToTypeMaps aaiToTypeMaps, 
            ISystemClock systemClock, 
            IExecutionContext executionContext, 
            IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _aaiToTypeMaps = aaiToTypeMaps;
            _systemClock = systemClock;
            _executionContext = executionContext;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

        /// <inheritdoc/>
        public IEventEnvelope CreateFrom(IEventSource eventSource, IEvent @event, EventSourceVersion version)
        {
            var envelope = new EventEnvelope(
                TransactionCorrelationId.NotSet,
                Guid.NewGuid(),
                EventSequenceNumber.Zero, 
                EventSequenceNumber.Zero, 
                _eventMigrationHierarchyManager.GetCurrentGenerationFor(@event.GetType()),
                _aaiToTypeMaps.Map(@event),
                eventSource.EventSourceId,
                _aaiToTypeMaps.Map(eventSource),
                version,
                _executionContext.Principal.Identity.Name,
                _systemClock.GetCurrentTime()
            );

            return envelope;
        }

        /// <inheritdoc/>
        public IEnumerable<IEventEnvelope> CreateFrom(IEventSource eventSource, IEnumerable<EventAndVersion> eventsAndVersion)
        {
            return eventsAndVersion.Select(e => CreateFrom(eventSource, e.Event, e.Version));
        }
    }
}
