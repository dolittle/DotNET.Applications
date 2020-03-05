// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using static contracts::Dolittle.Runtime.Events.EventStore;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>.
    /// </summary>
    /// <remarks>This implementation has been placed here temporarily.  It is not where it should be.</remarks>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        readonly EventStoreClient _eventStoreClient;
        readonly IEventConverter _eventConverter;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UncommittedEventStreamCoordinator"/> class.
        /// </summary>
        /// <param name="eventStoreClient">A see cref="EventStoreClient"/> for connecting to the runtime.</param>
        /// <param name="eventConverter">The <see cref="IEventConverter" />.</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging.</param>
        public UncommittedEventStreamCoordinator(
            EventStoreClient eventStoreClient,
            IEventConverter eventConverter,
            ILogger logger)
        {
            _eventStoreClient = eventStoreClient;
            _eventConverter = eventConverter;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Commit(CorrelationId correlationId, UncommittedAggregateEvents events)
        {
            _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
            _eventStoreClient.CommitForAggregate(_eventConverter.ToProtobuf(events));
        }
    }
}