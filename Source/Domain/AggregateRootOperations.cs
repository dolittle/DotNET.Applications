/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Rules;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents an implementation of <see cref="IAggregateRootOperations{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IAggregateRoot"/> type</typeparam>
    public class AggregateRootOperations<T> : IAggregateRootOperations<T>
        where T : class, IAggregateRoot
    {
        private readonly T _aggregateRoot;

        /// <summary>
        /// Initializes a new instance of <see cref="AggregateRootOperations{T}"/>
        /// </summary>
        /// <param name="aggregateRoot"><see cref="IAggregateRoot"/> the operations are for</param>
        public AggregateRootOperations(T aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        /// <inheritdoc/>
        public AggregateRootPerformResult Perform(Action<T> method)
        {
            var before = _aggregateRoot.RuleSetEvaluations.ToArray();
            method(_aggregateRoot);
            var after = _aggregateRoot.RuleSetEvaluations.ToArray();
            var evaluation = after.Except(before).SingleOrDefault();
            return new AggregateRootPerformResult(evaluation??new RuleSetEvaluation(new RuleSet(new IRule[0])));
        }
    }
}
