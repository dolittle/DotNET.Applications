// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that knows about event processors.
    /// </summary>
    public interface IEventProcessors
    {
        /// <summary>
        /// Registers and starts processing an event processor that processes events in a stream.
        /// </summary>
        /// <param name="scope">The <see cref="ScopeId" />.</param>
        /// <param name="sourceStream">The source <see cref="StreamId" />.</param>
        /// <param name="processor">The unique id of the processor.</param>
        /// <param name="call">The streaming call.</param>
        /// <param name="responseProperty">The response property.</param>
        /// <param name="requestProperty">The request property.</param>
        /// <param name="createProcessingRequestProxy">The callback for creating the processing request proxy.</param>
        /// <param name="createProcessingResponseProxy">The callback for creating the processing response proxy.</param>
        /// <param name="onFailedProcessing">The callback for when processing fo an event failed.</param>
        /// <param name="invoke">The callback for invoking the event on the event processor.</param>
        /// <typeparam name="TResponse">The response <see cref="IMessage" /> type.</typeparam>
        /// <typeparam name="TRequest">The request <see cref="IMessage" /> type.</typeparam>
        /// <typeparam name="TProcessingResult">The <see cref="IProcessingResult" /> type.</typeparam>
        void RegisterAndStartProcessing<TResponse, TRequest, TProcessingResult>(
            ScopeId scope,
            StreamId sourceStream,
            Guid processor,
            AsyncDuplexStreamingCall<TResponse, TRequest> call,
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            CreateProcessingRequestProxy<TRequest> createProcessingRequestProxy,
            CreateProcessingResponseProxy<TResponse, TRequest, TProcessingResult> createProcessingResponseProxy,
            OnFailedProcessing<TResponse, TRequest, TProcessingResult> onFailedProcessing,
            InvokeEventProcessing invoke)
            where TResponse : IMessage
            where TRequest : IMessage
            where TProcessingResult : IProcessingResult;
    }
}