// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Dolittle.Rules;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IAggregateRootOperations{T}"/>.
    /// </summary>
    /// <typeparam name="TAggregate"><see cref="AggregateRoot"/> type.</typeparam>
    public class AggregateRootOperations<TAggregate> : IAggregateRootOperations<TAggregate>
        where TAggregate : AggregateRoot
    {
        readonly TAggregate _aggregateRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootOperations{TAggregate}"/> class.
        /// </summary>
        /// <param name="aggregateRoot"><see cref="AggregateRoot"/> the operations are for.</param>
        public AggregateRootOperations(TAggregate aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        /// <inheritdoc/>
        public AggregateRootPerformResult Perform(Action<TAggregate> method)
        {
            var before = _aggregateRoot.RuleSetEvaluations.ToArray();
            method(_aggregateRoot);
            var after = _aggregateRoot.RuleSetEvaluations.ToArray();
            var evaluation = after.Except(before).SingleOrDefault();
            return new AggregateRootPerformResult(evaluation ?? new RuleSetEvaluation(new RuleSet(Array.Empty<IRule>())));
        }
    }
}
