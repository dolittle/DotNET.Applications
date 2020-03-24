// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Invokes the processing of an event.
    /// </summary>
    /// <param name="event">The <see cref="CommittedEvent" />.</param>
    /// <returns>A task that yields an <see cref="IProcessingResult" />.</returns>
    public delegate Task<TProcessingResult> InvokeEventProcessing<TProcessingResult>(CommittedEvent @event)
        where TProcessingResult : IProcessingResult;
}