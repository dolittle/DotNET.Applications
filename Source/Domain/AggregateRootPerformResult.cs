/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Rules;

namespace Dolittle.Domain
{
    /// <summary>
    /// Represents the result after a perform operation done on an <see cref="IAggregateRoot"/>
    /// </summary>
    public class AggregateRootPerformResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AggregateRootPerformResult"/>
        /// </summary>
        /// <param name="rulesResult"><see cref="RuleSetEvaluation"/> as result</param>
        public AggregateRootPerformResult(RuleSetEvaluation rulesResult)
        {
            RulesResult = rulesResult;
        }

        /// <summary>
        /// Gets the <see cref="RuleSetEvaluation"/> result
        /// </summary>
        public RuleSetEvaluation RulesResult { get; }

        /// <summary>
        /// Gets whether or not the perform was successful
        /// </summary>
        public bool IsSuccess => RulesResult.IsSuccess;

        /// <summary>
        /// Implicitly convert from <see cref="AggregateRootPerformResult"/> to boolean for success
        /// </summary>
        /// <param name="result"><see cref="AggregateRootPerformResult"/> to convert</param>
        public static implicit operator bool(AggregateRootPerformResult result) => result.IsSuccess;
    }
}
