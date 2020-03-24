// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Protobuf;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents the <see cref="ProcessingResponseProxy{TResponse, TRequest, TProcessingResult}" /> for <see cref="FilterClientToRuntimeResponse" />.
    /// </summary>
    public class PrivateEventsProcessingResponseProxy : ProcessingResponseProxy<FilterClientToRuntimeResponse, FilterRuntimeToClientRequest, IFilterResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateEventsProcessingResponseProxy"/> class.
        /// </summary>
        /// <param name="result">The <see cref="IFilterResult" />.</param>
        /// <param name="request">The <see cref="FilterRuntimeToClientRequest" />.</param>
        public PrivateEventsProcessingResponseProxy(IFilterResult result, FilterRuntimeToClientRequest request)
            : base(result, request)
        {
        }

        /// <inheritdoc/>
        public override FilterClientToRuntimeResponse ToResponse()
        {
            var response = new FilterClientToRuntimeResponse
            {
                ExecutionContext = Request.ExecutionContext,
                IsIncluded = ProcessingResult.IsIncluded,
                Partition = ProcessingResult.Partition.ToProtobuf()
            };
            if (ProcessingResult.Succeeded)
            {
                response.Succeeded = true;
                response.Retry = false;
            }
            else if (ProcessingResult is IRetryFilteringResult retryFilteringResult)
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