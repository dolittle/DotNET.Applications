// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a system that can process an event processing request.
    /// </summary>
    /// <typeparam name="TResponse">The response <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
    /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
    public class EventProcessor<TResponse, TRequest, TProcessingResult>
        where TResponse : IMessage
        where TRequest : IMessage
        where TProcessingResult : IProcessingResult
    {
        readonly CreateProcessingRequestProxy<TRequest> _createProcessingRequestProxy;
        readonly CreateProcessingResponseProxy<TResponse, TRequest, TProcessingResult> _createProcessingResponseProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessor{TResponse, TRequest, TProcessingResult}"/> class.
        /// </summary>
        /// <param name="identifier">The <see cref="EventProcessorId" />.</param>
        /// <param name="streamingCall">The streaming call.</param>
        /// <param name="invocationManager">The <see cref="IEventProcessingInvocationManager{TProcessingResult}" />.</param>
        /// <param name="responseProperty">The response property expression.</param>
        /// <param name="requestProperty">The request property expression.</param>
        /// <param name="createProcessingRequestProxy">The callback for creating <see cref="ProcessingRequestProxy{TRequest}" /> for the request type.</param>
        /// <param name="createProcessingResponseProxy">The callback for creating <see cref="ProcessingResponseProxy{TResponse, TRequest, TProcessingResult}" /> for the response type.</param>
        public EventProcessor(
            EventProcessorId identifier,
            AsyncDuplexStreamingCall<TResponse, TRequest> streamingCall,
            IEventProcessingInvocationManager<TProcessingResult> invocationManager,
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            CreateProcessingRequestProxy<TRequest> createProcessingRequestProxy,
            CreateProcessingResponseProxy<TResponse, TRequest, TProcessingResult> createProcessingResponseProxy)
        {
            _createProcessingRequestProxy = createProcessingRequestProxy;
            _createProcessingResponseProxy = createProcessingResponseProxy;
            Identifier = identifier;
            StreamingCall = streamingCall;
            ResponseProperty = responseProperty;
            RequestProperty = requestProperty;
            InvocationManager = invocationManager;
        }

        /// <summary>
        /// Gets the <see cref="EventProcessorId" />.
        /// </summary>
        public EventProcessorId Identifier { get; }

        /// <summary>
        /// Gets the streaming call.
        /// </summary>
        public AsyncDuplexStreamingCall<TResponse, TRequest> StreamingCall { get; }

        /// <summary>
        /// Gets the response property expression.
        /// </summary>
        public Expression<Func<TResponse, ulong>> ResponseProperty { get; }

        /// <summary>
        /// Gets the request property expression.
        /// </summary>
        public Expression<Func<TRequest, ulong>> RequestProperty { get; }

        /// <summary>
        /// Gets the <see cref="IEventProcessingInvocationManager{TProcessingResult}" />.
        /// </summary>
        public IEventProcessingInvocationManager<TProcessingResult> InvocationManager { get; }

        /// <summary>
        /// Processes the event.
        /// </summary>
        /// <param name="requestProxy">The <see typeref="ProcessingRequestProxy{TRequest}" />.</param>
        /// <param name="event">The <see cref="CommittedEvent "/>.</param>
        /// <returns>A task that yields <see cref="IProcessingResult" />.</returns>
        public async Task<TResponse> ProcessRequest(ProcessingRequestProxy<TRequest> requestProxy, CommittedEvent @event)
        {
            var processingResult = await InvocationManager.Invoke(@event, requestProxy.Partition).ConfigureAwait(false);
            return _createProcessingResponseProxy(processingResult, requestProxy.Request);
        }

        /// <summary>
        /// Gets the <see cref="ProcessingRequestProxy{TRequest}" /> from the given request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The <see cref="ProcessingRequestProxy{TRequest}" />.</returns>
        public ProcessingRequestProxy<TRequest> ProxyFromRequest(TRequest request) => _createProcessingRequestProxy(request);
    }
}