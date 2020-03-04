// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a systemt that knows about all <see cref="EventProcessorPartitionContext" />.
    /// </summary>
    public interface IEventProcessorPartitionContexts
    {
        /// <summary>
        /// Gets the <see cref="EventProcessorPartitionContext" /> for a partition in an <see cref="ICanProcessEvent" /> event processor.
        /// </summary>
        /// <param name="processor">The <see cref="ICanProcessEvent" />.</param>
        /// <param name="partition">The <see cref="PartitionId" />.</param>
        /// <returns>The <see cref="EventProcessorPartitionContext" />.</returns>
        EventProcessorPartitionContext GetContextFor(ICanProcessEvent processor, PartitionId partition);
    }
}