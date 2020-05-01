// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Dolittle.Commands.Coordination;
using Dolittle.Events;
using Dolittle.Logging;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IAggregateOf{T}"/>.
    /// </summary>
    /// <typeparam name="TAggregate">Type of <see cref="AggregateRoot"/>.</typeparam>
    public class AggregateOf<TAggregate> : IAggregateOf<TAggregate>
        where TAggregate : AggregateRoot
    {
        readonly ICommandContextManager _commandContextManager;
        readonly IEventStore _eventStore;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateOf{T}"/> class.
        /// </summary>
        /// <param name="commandContextManager"> <see cref="ICommandContextManager"/> to use for tracking.</param>
        /// <param name="eventStore">The <see cref="IEventStore" />.</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging.</param>
        public AggregateOf(
            ICommandContextManager commandContextManager,
            IEventStore eventStore,
            ILogger logger)
        {
            _commandContextManager = commandContextManager;
            _eventStore = eventStore;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Create()
        {
            var aggregate = Get(Guid.NewGuid());
            return new AggregateRootOperations<TAggregate>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Create(EventSourceId eventSourceId)
        {
            var aggregate = Get(eventSourceId);
            return new AggregateRootOperations<TAggregate>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Rehydrate(EventSourceId eventSourceId)
        {
            var aggregate = Get(eventSourceId);
            return new AggregateRootOperations<TAggregate>(aggregate);
        }

        TAggregate Get(EventSourceId id)
        {
            _logger.Trace($"Get '{typeof(TAggregate).AssemblyQualifiedName}' with Id of '{id?.Value.ToString() ?? "<unknown id>"}'");

            var commandContext = _commandContextManager.GetCurrent();
            var type = typeof(TAggregate);
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

        void ReApplyEvents(TAggregate aggregateRoot)
        {
            var eventSourceId = aggregateRoot.EventSourceId;
            var committedEvents = _eventStore.FetchForAggregate<TAggregate>(eventSourceId, CancellationToken.None).GetAwaiter().GetResult();
            if (committedEvents.HasEvents)
                aggregateRoot.ReApply(committedEvents);
        }

        TAggregate GetInstanceFrom(EventSourceId id, ConstructorInfo constructor)
        {
            return (constructor.GetParameters()[0].ParameterType == typeof(EventSourceId) ?
                constructor.Invoke(new object[] { id }) :
                constructor.Invoke(new object[] { id.Value })) as TAggregate;
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
    }
}
