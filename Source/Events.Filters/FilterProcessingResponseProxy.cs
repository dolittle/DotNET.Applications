// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Events.Processing;
using Dolittle.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents the <see cref="ProcessingResponseProxy{TResponse, TRequest, TProcessingResult}" /> for <see cref="FilterClientToRuntimeResponse" />.
    /// </summary>
    public class FilterProcessingResponseProxy : ProcessingResponseProxy<FilterClientToRuntimeResponse, FilterRuntimeToClientRequest, IFilterResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterProcessingResponseProxy"/> class.
        /// </summary>
        /// <param name="result">The <see cref="IFilterResult" />.</param>
        /// <param name="request">The <see cref="FilterRuntimeToClientRequest" />.</param>
        public FilterProcessingResponseProxy(IFilterResult result, FilterRuntimeToClientRequest request)
            : base(result, request)
        {
        }

        /// <inheritdoc/>
        public override FilterClientToRuntimeResponse ToResponse()
        {
            var response = new FilterClientToRuntimeResponse
            {
                ExecutionContext = Request.ExecutionContext
            };
            if (ProcessingResult.Succeeded)
            {
                response.Success = new SuccessfulFilter { IsIncluded = true, Partition = ProcessingResult.Partition.ToProtobuf() };
            }
            else if (ProcessingResult is IRetryFilteringResult retryFilteringResult)
            {
                response.Failed = new ProcessorFailure { Retry = true, RetryTimeout = Duration.FromTimeSpan(retryFilteringResult.RetryTimeout), Reason = retryFilteringResult.FailureReason };
            }
            else
            {
                response.Failed = new ProcessorFailure { Retry = false, Reason = ProcessingResult.FailureReason };
            }

            return response;
        }
    }
}