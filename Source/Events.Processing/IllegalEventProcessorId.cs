// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Exception that gets thrown when <see cref="EventProcessorId" /> is an illegal value.
    /// </summary>
    public class IllegalEventProcessorId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalEventProcessorId"/> class.
        /// </summary>
        /// <param name="eventProcessorId">The <see cref="EventProcessorId" />.</param>
        public IllegalEventProcessorId(EventProcessorId eventProcessorId)
            : base($"Event processor id cannot be '{eventProcessorId}'")
        {
        }
    }
}