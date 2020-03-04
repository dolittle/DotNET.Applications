// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines the result of the processing of a <see cref="CommittedEvent" />.
    /// </summary>
    public class ProcessingResult : Value<ProcessingResult>
    {
        /// <summary>
        /// Gets a value indicating whether processing succeeded.
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// Gets the reason for failure.
        /// </summary>
        public string FailureReason { get; }
    }
}