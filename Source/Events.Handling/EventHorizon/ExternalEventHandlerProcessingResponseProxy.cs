// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Represents the <see cref="ProcessingResponseProxy{TResponse, TRequest, TProcessingResult}" /> for <see cref="ScopedFilterClientToRuntimeResponse" />.
    /// </summary>
    public class ExternalEventHandlerProcessingResponseProxy : ProcessingResponseProxy<ScopedEventHandlerClientToRuntimeResponse, ScopedEventHandlerRuntimeToClientRequest, IProcessingResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProcessingResponseProxy"/> class.
        /// </summary>
        /// <param name="result">The <see cref="IProcessingResult" />.</param>
        /// <param name="request">The <see cref="ScopedEventHandlerRuntimeToClientRequest" />.</param>
        public ExternalEventHandlerProcessingResponseProxy(IProcessingResult result, ScopedEventHandlerRuntimeToClientRequest request)
            : base(result, request)
        {
        }

        /// <inheritdoc/>
        public override ScopedEventHandlerClientToRuntimeResponse ToResponse()
        {
            var response = new ScopedEventHandlerClientToRuntimeResponse
            {
                ExecutionContext = Request.ExecutionContext
            };
            if (ProcessingResult.Succeeded)
            {
                response.Succeeded = true;
                response.Retry = false;
            }
            else if (ProcessingResult is IRetryProcessingResult retryFilteringResult)
            {
                response.Succeeded = false;
                response.Retry = true;
                response.RetryTimeout = retryFilteringResult.RetryTimeout;
            }
            else
            {
                response.Succeeded = false;
                response.Retry = false;
            }

            return response;
        }
    }
}