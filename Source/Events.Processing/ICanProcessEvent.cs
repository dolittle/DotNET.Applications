// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that can process a <see cref="CommittedEvent" />.
    /// </summary>
    public interface ICanProcessEvent
    {
        /// <summary>
        /// Processes a <see cref="CommittedEvent" />.
        /// </summary>
        /// <param name="event">The <see cref="CommittedEvent" />.</param>
        /// <param name="partition">The <see cref="PartitionId" />.</param>
        /// <returns>The <see cref="ProcessingResult" />.</returns>
        Task<ProcessingResult> Process(CommittedEvent @event, PartitionId partition);
    }
}