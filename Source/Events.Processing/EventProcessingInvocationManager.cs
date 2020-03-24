// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingInvocationManager" />.
    /// </summary>
    public class EventProcessingInvocationManager : IEventProcessingInvocationManager
    {
        readonly InvokeEventProcessing _invoke;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessingInvocationManager"/> class.
        /// </summary>
        /// <param name="invoke">The <see cref="InvokeEventProcessing" /> callback.</param>
        public EventProcessingInvocationManager(InvokeEventProcessing invoke)
        {
            _invoke = invoke;
        }

        /// <inheritdoc/>
        public async Task<IProcessingResult> Invoke(CommittedEvent @event, PartitionId partition)
        {
            try
            {
                return await _invoke(@event).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // TODO: Check number of retries. Add retry timeout.
                return new FailedProcessingResult($"Failure Message: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }
    }
}