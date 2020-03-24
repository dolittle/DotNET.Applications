// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingInvocationManager{TProcessing}" />.
    /// </summary>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
    public class EventProcessingInvocationManager<TProcessingResult> : IEventProcessingInvocationManager<TProcessingResult>
        where TProcessingResult : IProcessingResult
    {
        readonly InvokeEventProcessing<TProcessingResult> _invoke;
        readonly Func<string, bool, uint, TProcessingResult> _onFailedProcessing;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessingInvocationManager{TProcessingResult}"/> class.
        /// </summary>
        /// <param name="invoke">The <see cref="InvokeEventProcessing{TProcessingResult}" /> callback.</param>
        /// <param name="onFailedProcessing">The callback for creating a failed processing result.</param>
        public EventProcessingInvocationManager(InvokeEventProcessing<TProcessingResult> invoke, Func<string, bool, uint, TProcessingResult> onFailedProcessing)
        {
            _invoke = invoke;
            _onFailedProcessing = onFailedProcessing;
        }

        /// <inheritdoc/>
        public async Task<TProcessingResult> Invoke(CommittedEvent @event, PartitionId partition)
        {
            try
            {
                return await _invoke(@event).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return _onFailedProcessing($"Failure Message: {ex.Message}\nStack Trace: {ex.StackTrace}", false, 0);
            }
        }
    }
}