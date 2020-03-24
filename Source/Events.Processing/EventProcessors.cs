// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessors" />.
    /// </summary>
    [Singleton]
    public class EventProcessors : IEventProcessors
    {
        readonly ConcurrentDictionary<EventProcessorId, bool> _registeredEventProcessors = new ConcurrentDictionary<EventProcessorId, bool>();
        readonly IEventStreamProcessors _eventStreamProcessors;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessors"/> class.
        /// </summary>
        /// <param name="eventStreamProcessors">The <see cref="IEventStreamProcessors" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public EventProcessors(IEventStreamProcessors eventStreamProcessors, ILogger logger)
        {
            _eventStreamProcessors = eventStreamProcessors;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void RegisterAndStartProcessing<TResponse, TRequest, TProcessingResult>(
            ScopeId scope,
            StreamId sourceStream,
            Guid processor,
            AsyncDuplexStreamingCall<TResponse, TRequest> call,
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            CreateProcessingRequestProxy<TRequest> createProcessingRequestProxy,
            CreateProcessingResponseProxy<TResponse, TRequest, TProcessingResult> createProcessingResponseProxy,
            Func<string, bool, uint, TProcessingResult> onFailedProcessing,
            InvokeEventProcessing<TProcessingResult> invoke)
            where TResponse : IMessage
            where TRequest : IMessage
            where TProcessingResult : IProcessingResult
        {
            var eventProcessorId = new EventProcessorId(scope, sourceStream, processor);
            _registeredEventProcessors.TryAdd(eventProcessorId, true);
            var invocationManager = new EventProcessingInvocationManager<TProcessingResult>(invoke, onFailedProcessing);

            var eventProcessor = new EventProcessor<TResponse, TRequest, TProcessingResult>(eventProcessorId, call, invocationManager, responseProperty, requestProperty, createProcessingRequestProxy, createProcessingResponseProxy);
            _eventStreamProcessors.StartProcessing(eventProcessor);
        }
    }
}