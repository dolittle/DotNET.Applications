/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IAggregateOf{T}"/>
    /// </summary>
    public class AggregateOf<T> : IAggregateOf<T>
        where T : class, IAggregateRoot
    {
        readonly IAggregateRootRepositoryFor<T> _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="AggregateOf{T}"/>
        /// </summary>
        /// <param name="repository"><see cref="IAggregateRootRepositoryFor{T}"/> for getting <see cref="IAggregateRoot"/> instance</param>
        public AggregateOf(IAggregateRootRepositoryFor<T> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<T> Create()
        {
            var aggregate = _repository.Get(Guid.NewGuid());
            return new AggregateRootOperations<T>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<T> Create(EventSourceId eventSourceId)
        {
            var aggregate = _repository.Get(eventSourceId);
            return new AggregateRootOperations<T>(aggregate);
        }

        /// <inheritdoc/>
        public IAggregateRootOperations<T> Rehydrate(EventSourceId eventSourceId)
        {
            var aggregate = _repository.Get(eventSourceId);
            return new AggregateRootOperations<T>(aggregate);
        }
    }
}
