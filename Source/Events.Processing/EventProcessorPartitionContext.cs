// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the context of a partition in in an event processor.
    /// </summary>
    public class EventProcessorPartitionContext : Value<EventProcessorPartitionContext>
    {
        /// <summary>
        /// Gets the number of times that a partition in an event processor has failed consecutively.
        /// </summary>
        public int ConsecutiveFailures { get; }
    }
}