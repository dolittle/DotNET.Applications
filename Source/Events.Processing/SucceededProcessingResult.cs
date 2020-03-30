// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IProcessingResult" /> where event processing succeeded.
    /// </summary>
    public class SucceededProcessingResult : IProcessingResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SucceededProcessingResult"/> class.
        /// </summary>
        public SucceededProcessingResult()
        {
        }

        /// <inheritdoc />
        public bool Succeeded => true;

        /// <inheritdoc />
        public bool Retry => false;

        /// <inheritdoc/>
        public string FailureReason => string.Empty;
    }
}