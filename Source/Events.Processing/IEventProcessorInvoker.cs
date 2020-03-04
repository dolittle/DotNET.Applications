// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that can handle the context around the invocation of an <see cref="ICanProcessEvent" /> event processor.
    /// </summary>
    public interface IEventProcessorInvoker
    {
        /// <summary>
        /// Invokes the process method of an <see cref="ICanProcessEvent" />.
        /// </summary>
        /// <param name="processor">The <see cref="ICanProcessEvent" />.</param>
        /// <param name="event">The <see cref="CommittedEvent" />.</param>
        /// <param name="partition">The <see cref="PartitionId" />.</param>
        /// <returns>The <see cref="ProcessingInvocationResult" />.</returns>
        Task<ProcessingInvocationResult> Invoke(ICanProcessEvent processor, CommittedEvent @event, PartitionId partition);
    }
}