// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Reflection;
using Google.Protobuf;
using grpc = contracts::Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingInvocationManagerFor{TProcessingResponse, TProcessingResult}" />.
    /// </summary>
    /// <typeparam name="TProcessingResponse">The processing response <see cref="IMessage" />.</typeparam>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" />.</typeparam>
    public class EventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult> : IEventProcessingInvocationManagerFor<TProcessingResponse, TProcessingResult>
        where TProcessingResponse : IMessage, new()
        where TProcessingResult : IProcessingResult
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessingInvocationManagerFor{TProcessingResponse, TProcessingResult}"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventProcessingInvocationManagerFor(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<TProcessingResponse> Invoke(
            Func<Task<TProcessingResult>> invoke,
            Type eventProcessorType,
            PartitionId partition,
            grpc.RetryProcessingState retryProcessingState,
            Func<TProcessingResult, TProcessingResponse> createResponseFromSucceededProcessingResult,
            Expression<Func<TProcessingResponse, grpc.ProcessorFailure>> processorFailurePropertyExpression)
        {
            var response = new TProcessingResponse();
            var processorFailureProperty = processorFailurePropertyExpression.GetPropertyInfo();
            try
            {
                var result = await invoke().ConfigureAwait(false);
                response = createResponseFromSucceededProcessingResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while processing event");
                processorFailureProperty.SetValue(response, new grpc.ProcessorFailure { FailureType = grpc.RetryProcessingState.Types.FailureType.Unknown, Reason = $"{ex.Message} - Stack Trace: {ex.StackTrace}", Retry = false });
            }

            return response;
        }
    }
}
