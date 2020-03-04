// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the result of the invocation of an <see cref="ICanProcessEvent" /> event processor.
    /// </summary>
    public class ProcessingInvocationResult : Value<ProcessingInvocationResult>
    {

        /// <summary>
        /// Gets a value indicating whether processing succeeded.
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// Gets the reason for failure.
        /// </summary>
        public string FailureReason { get; }

        /// <summary>
        /// Gets a value indicating whether to retry processing.
        /// </summary>
        public bool Retry { get; }

        /// <summary>
        /// Gets the amount milliseconds from now for when next retry should occur.
        /// </summary>
        public uint RetryTimeout { get; }
    }
}