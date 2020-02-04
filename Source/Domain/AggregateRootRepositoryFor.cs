// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.Events;
using Dolittle.Logging;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Runtime.Events;
using Dolittle.Runtime.Events.Store;

#pragma warning disable CS0612, CS0618

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines a concrete implementation of <see cref="IAggregateRootRepositoryFor{T}">IAggregatedRootRepository</see>.
    /// </summary>
    /// <typeparam name="T">Type the repository is for.</typeparam>
    [Obsolete("Use of AggregateRootRepositoryFor is being replaced by AggregateOf and will be removed in a future version", false)]
    public class AggregateRootRepositoryFor<T> : IAggregateRootRepositoryFor<T>
        where T : class, IAggregateRoot
    {
        readonly ICommandContextManager _commandContextManager;
        readonly IEventStore _eventStore;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ILogger _logger;
        readonly IObjectFactory _objectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootRepositoryFor{T}"/> class.
        /// </summary>
        /// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking.</param>
        /// <param name="eventStore"><see cref="IEventStore"/> for getting <see cref="IEvent">events</see>.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for being able to identify resources.</param>
        /// <param name="objectFactory"><see cref="IObjectFactory"/> to construct an instance of a Type from a <see cref="PropertyBag" />.</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging.</param>
        public AggregateRootRepositoryFor(
            ICommandContextManager commandContextManager,
            IEventStore eventStore,
            IArtifactTypeMap artifactTypeMap,
            IObjectFactory objectFactory,
            ILogger logger)
        {
            _commandContextManager = commandContextManager;
            _eventStore = eventStore;
            _artifactTypeMap = artifactTypeMap;
            _logger = logger;
            _objectFactory = objectFactory;
        }

        /// <inheritdoc/>
        public T Get(EventSourceId id)
        {
            _logger.Trace($"Get '{typeof(T).AssemblyQualifiedName}' with Id of '{id?.Value.ToString() ?? "<unknown id>"}'");

            var commandContext = _commandContextManager.GetCurrent();
            var type = typeof(T);
            var constructor = GetConstructorFor(type);
            ThrowIfConstructorIsInvalid(type, constructor);

            var aggregateRoot = GetInstanceFrom(id, constructor);
            if (aggregateRoot != null)
            {
                ReApplyEvents(aggregateRoot);
            }

            commandContext.RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        void ReApplyEvents(T aggregateRoot)
        {
            var identifier = _artifactTypeMap.GetArtifactFor(typeof(T));
            var commits = _eventStore.Fetch(new EventSourceKey(aggregateRoot.EventSourceId, identifier.Id));
            var committedEvents = new CommittedEvents(aggregateRoot.EventSourceId, FromCommits(commits));
            if (committedEvents.HasEvents)
                aggregateRoot.ReApply(committedEvents);
        }

        T GetInstanceFrom(EventSourceId id, ConstructorInfo constructor)
        {
            return (constructor.GetParameters()[0].ParameterType == typeof(EventSourceId) ?
                constructor.Invoke(new object[] { id }) :
                constructor.Invoke(new object[] { id.Value })) as T;
        }

        ConstructorInfo GetConstructorFor(Type type)
        {
            return type.GetTypeInfo().GetConstructors().SingleOrDefault(c =>
            {
                var parameters = c.GetParameters();
                return parameters.Length == 1 &&
                    (parameters[0].ParameterType == typeof(Guid) ||
                        parameters[0].ParameterType == typeof(EventSourceId));
            });
        }

        void ThrowIfConstructorIsInvalid(Type type, ConstructorInfo constructor)
        {
            if (constructor == null) throw new InvalidAggregateRootConstructorSignature(type);
        }

        IEnumerable<CommittedEvent> FromCommits(Commits commits)
        {
            var events = new List<CommittedEvent>();

            foreach (var commit in commits)
            {
                foreach (var @event in commit.Events)
                {
                    events.Add(ToCommittedEvent(commit.Sequence, @event));
                }
            }

            return events;
        }

        CommittedEvent ToCommittedEvent(CommitSequenceNumber commitSequenceNumber, EventEnvelope @event)
        {
            var eventType = _artifactTypeMap.GetTypeFor(@event.Metadata.Artifact);
            var eventInstance = _objectFactory.Build(eventType, @event.Event) as IEvent;
            var committedEventVersion = new CommittedEventVersion(commitSequenceNumber, @event.Metadata.VersionedEventSource.Version.Commit, @event.Metadata.VersionedEventSource.Version.Sequence);
            return new CommittedEvent(committedEventVersion, @event.Metadata, eventInstance);
        }
    }
}