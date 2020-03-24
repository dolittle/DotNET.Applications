// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that manages the invocations.
    /// </summary>
    public interface IEventProcessingInvocationManager
    {
        /// <summary>
        /// Manages the invocation of the processing of an event in a stream.
        /// </summary>
        /// <param name="event">The <see cref="CommittedEvent" />.</param>
        /// <param name="partition">The <see cref="PartitionId" />.</param>
        /// <returns>A task that yields a <see cref="IProcessingResult" />.</returns>
        Task<IProcessingResult> Invoke(CommittedEvent @event, PartitionId partition);
    }
}