// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Rules;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents the result after a perform operation done on an <see cref="IAggregateRoot"/>.
    /// </summary>
    public class AggregateRootPerformResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootPerformResult"/> class.
        /// </summary>
        /// <param name="rulesResult"><see cref="RuleSetEvaluation"/> as result.</param>
        public AggregateRootPerformResult(RuleSetEvaluation rulesResult)
        {
            RulesResult = rulesResult;
        }

        /// <summary>
        /// Gets the <see cref="RuleSetEvaluation"/> result.
        /// </summary>
        public RuleSetEvaluation RulesResult { get; }

        /// <summary>
        /// Gets a value indicating whether or not the perform was successful.
        /// </summary>
        public bool IsSuccess => RulesResult.IsSuccess;

        /// <summary>
        /// Implicitly convert from <see cref="AggregateRootPerformResult"/> to boolean for success.
        /// </summary>
        /// <param name="result"><see cref="AggregateRootPerformResult"/> to convert.</param>
        public static implicit operator bool(AggregateRootPerformResult result) => result.IsSuccess;
    }
}
