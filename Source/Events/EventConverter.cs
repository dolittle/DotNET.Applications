// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Contracts = Dolittle.Runtime.Events.Contracts;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventConverter"/>.
    /// </summary>
    public class EventConverter : IEventConverter
    {
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventConverter"/> class.
        /// </summary>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="serializer"><see cref="ISerializer"/> for serialization.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventConverter(
            IArtifactTypeMap artifactTypeMap,
            ISerializer serializer,
            ILogger logger)
        {
            _artifactTypeMap = artifactTypeMap;
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Contracts.UncommittedEvent ToProtobuf(UncommittedEvent @event)
            {
                try
                {
                    return new Contracts.UncommittedEvent
                    {
                        Artifact = ToProtobuf(@event.Event.GetType()),
                        EventSourceId = @event.EventSource.ToProtobuf(),
                        Public = IsPublicEvent(@event.Event),
                        Content = _serializer.EventToJson(@event.Event),
                    };
                }
                catch (UnableToInstantiateInstanceOfType ex)
                {
                    _logger.Warning(ex, "Couldn't serialize uncommitted event: {Type} to Json.", @event.Event.GetType());
                    throw;
                }
            }

        /// <inheritdoc/>
        public IEnumerable<Contracts.UncommittedEvent> ToProtobuf(UncommittedEvents events)
            => events.Select(_ => ToProtobuf(_));

        /// <inheritdoc/>
        public Contracts.UncommittedAggregateEvents ToProtobuf(UncommittedAggregateEvents uncommittedEvents)
        {
            var aggregateRoot = _artifactTypeMap.GetArtifactFor(uncommittedEvents.AggregateRoot);
            var events = new Contracts.UncommittedAggregateEvents
            {
                AggregateRootId = aggregateRoot.Id.ToProtobuf(),
                EventSourceId = uncommittedEvents.EventSource.ToProtobuf(),
                ExpectedAggregateRootVersion = uncommittedEvents.ExpectedAggregateRootVersion,
            };

            foreach (var @event in uncommittedEvents)
            {
                try
                {
                    events.Events.Add(new Contracts.UncommittedAggregateEvents.Types.UncommittedAggregateEvent
                    {
                        Artifact = ToProtobuf(@event.GetType()),
                        Public = IsPublicEvent(@event),
                        Content = _serializer.EventToJson(@event),
                    });
                }
                catch (UnableToInstantiateInstanceOfType ex)
                {
                    _logger.Warning(ex, "Couldn't serialize uncommitted aggregate event: {Type} to Json.", @event.GetType());
                    throw;
                }
            }

            return events;
        }

        /// <inheritdoc/>
        public CommittedEvent ToSDK(Contracts.CommittedEvent source)
        {
                var artifact = ToSDK(source.Type);
                try
                {
                    var @event = _serializer.JsonToEvent(artifact, source.Content);
                    if (source.External)
                    {
                        return new CommittedExternalEvent(
                            source.EventLogSequenceNumber,
                            source.Occurred.ToDateTimeOffset(),
                            source.EventSourceId.To<EventSourceId>(),
                            source.ExecutionContext.ToExecutionContext(),
                            source.ExternalEventLogSequenceNumber,
                            source.ExternalEventReceived.ToDateTimeOffset(),
                            @event);
                    }
                    else
                    {
                        return new CommittedEvent(
                            source.EventLogSequenceNumber,
                            source.Occurred.ToDateTimeOffset(),
                            source.EventSourceId.To<EventSourceId>(),
                            source.ExecutionContext.ToExecutionContext(),
                            @event);
                    }
                }
                catch (UnableToInstantiateInstanceOfType ex)
                {
                    _logger.Warning(ex, "Couldn't deserialize artifact: {Artifact} to a committed event.", artifact);
                    throw;
                }
        }

        /// <inheritdoc/>
        public CommittedEvents ToSDK(IEnumerable<Contracts.CommittedEvent> source)
            => new CommittedEvents(source.Select(ToSDK).ToList());

        /// <inheritdoc/>
        public CommittedAggregateEvents ToSDK(Contracts.CommittedAggregateEvents source)
        {
            var aggregateRootVersion = source.AggregateRootVersion - (ulong)source.Events.Count + 1;
            var aggregateRoot = _artifactTypeMap.GetTypeFor(new Artifact(source.AggregateRootId.To<ArtifactId>(), ArtifactGeneration.First));

            var events = source.Events.Select(eventSource =>
            {
                    var artifact = ToSDK(eventSource.Type);
                    try
                    {
                        var @event = _serializer.JsonToEvent(artifact, eventSource.Content);
                        return new CommittedAggregateEvent(
                            eventSource.EventLogSequenceNumber,
                            eventSource.Occurred.ToDateTimeOffset(),
                            source.EventSourceId.To<EventSourceId>(),
                            aggregateRoot,
                            aggregateRootVersion++,
                            eventSource.ExecutionContext.ToExecutionContext(),
                            @event);
                    }
                    catch (UnableToInstantiateInstanceOfType ex)
                    {
                        _logger.Warning(ex, "Couldn't deserialize artifact {Artifact} to CommittedAggregateEvent.", artifact);
                        throw;
                    }
            }).ToList();

            return new CommittedAggregateEvents(
                source.EventSourceId.To<EventSourceId>(),
                aggregateRoot,
                events);
        }

        Artifacts.Contracts.Artifact ToProtobuf(Type artifact)
        {
            var mapped = _artifactTypeMap.GetArtifactFor(artifact);
            return new Artifacts.Contracts.Artifact
            {
                Id = mapped.Id.ToProtobuf(),
                Generation = mapped.Generation,
            };
        }

        Type ToSDK(Artifacts.Contracts.Artifact artifact)
            => _artifactTypeMap.GetTypeFor(new Artifact(artifact.Id.To<ArtifactId>(), artifact.Generation));

        bool IsPublicEvent(IEvent @event)
            => typeof(IPublicEvent).IsAssignableFrom(@event.GetType());
    }
}