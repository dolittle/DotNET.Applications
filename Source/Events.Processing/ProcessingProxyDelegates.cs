// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Creates a processing request proxy from request.
    /// </summary>
    /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
    /// <param name="request">The request.</param>
    public delegate ProcessingRequestProxy<TRequest> CreateProcessingRequestProxy<TRequest>(TRequest request)
        where TRequest : IMessage;

    /// <summary>
    /// Creates a processing response proxy from <see cref="IProcessingResult" />..
    /// </summary>
    /// <typeparam name="TResponse">The response <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
    /// <param name="processingResult">The <see cref="IProcessingResult" />.</param>
    /// <param name="request">The processing request.</param>
    public delegate ProcessingResponseProxy<TResponse, TRequest, TProcessingResult> CreateProcessingResponseProxy<TResponse, TRequest, TProcessingResult>(TProcessingResult processingResult, TRequest request)
        where TResponse : IMessage
        where TRequest : IMessage
        where TProcessingResult : IProcessingResult;
}