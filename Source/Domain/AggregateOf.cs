// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IAggregateOf{T}"/>.
    /// </summary>
    /// <typeparam name="TAggregate">Type of <see cref="IAggregateRoot"/>.</typeparam>
    public class AggregateOf<TAggregate> : IAggregateOf<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        readonly IAggregateRootRepositoryFor<TAggregate> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateOf{T}"/> class.
        /// </summary>
        /// <param name="repository"><see cref="IAggregateRootRepositoryFor{T}"/> for getting <see cref="IAggregateRoot"/> instance.</param>
        public AggregateOf(IAggregateRootRepositoryFor<TAggregate> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Create()
        {
            var aggregate = _repository.Get(Guid.NewGuid());
            return new AggregateRootOperations<TAggregate>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Create(EventSourceId eventSourceId)
        {
            var aggregate = _repository.Get(eventSourceId);
            return new AggregateRootOperations<TAggregate>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<TAggregate> Rehydrate(EventSourceId eventSourceId)
        {
            var aggregate = _repository.Get(eventSourceId);
            return new AggregateRootOperations<TAggregate>(aggregate);
        }
    }
}
