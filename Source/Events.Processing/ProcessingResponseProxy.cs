// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the proxy object for a event processing response.
    /// </summary>
    /// <typeparam name="TResponse">The response <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
    public abstract class ProcessingResponseProxy<TResponse, TRequest, TProcessingResult>
        where TResponse : IMessage
        where TRequest : IMessage
        where TProcessingResult : IProcessingResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingResponseProxy{TResponse, TRequest, TProcessingResult}"/> class.
        /// </summary>
        /// <param name="processingResult">The <see cref="IProcessingResult" />.>.</param>
        /// <param name="request">The initial request.</param>
        protected ProcessingResponseProxy(TProcessingResult processingResult, TRequest request)
        {
            ProcessingResult = processingResult;
            Request = request;
        }

        /// <summary>
        /// Gets the <see cref="IProcessingResult" />.
        /// </summary>
        public TProcessingResult ProcessingResult { get; }

        /// <summary>
        /// Gets the request.
        /// </summary>
        public TRequest Request { get; }

        /// <summary>
        /// Converts the <see cre="ProcessingResponseProxy{TResponse, TPRocessingResult}" /> to the <see typeparam="TResponse" /> <see cref="IMessage" />.
        /// </summary>
        /// <param name="proxy">The <see cre="ProcessingResponseProxy{TResponse, TPRocessingResult}" />.</param>
        public static implicit operator TResponse(ProcessingResponseProxy<TResponse, TRequest, TProcessingResult> proxy) => proxy.ToResponse();

        /// <summary>
        /// Converts the <see cref="ProcessingResponseProxy{TResponse, TRequest, TPRocessingResult}" /> to the correct event processing response.
        /// </summary>
        /// <returns>The event processing response.</returns>
        public abstract TResponse ToResponse();
    }
}