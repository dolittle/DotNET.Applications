// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the result of the evaluation of a <see cref="RetryStrategy" />.
    /// </summary>
    public class RetryStrategyResult : Value<RetryStrategyResult>
    {
        /// <summary>
        /// Gets a value indicating whether to retry.
        /// </summary>
        public bool Retry { get; }

        /// <summary>
        /// Gets the amount milliseconds from now for when next retry should occur.
        /// </summary>
        public uint RetryTimeout { get; }
    }
}