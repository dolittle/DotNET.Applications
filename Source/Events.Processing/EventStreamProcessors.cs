// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Services.Clients;
using Google.Protobuf;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStreamProcessors" />.
    /// </summary>
    public class EventStreamProcessors : IEventStreamProcessors
    {
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStreamProcessors"/> class.
        /// </summary>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing the <see cref="ExecutionContext"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventStreamProcessors(
            IReverseCallClientManager reverseCallClientManager,
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _reverseCallClientManager = reverseCallClientManager;
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void StartProcessing<TResponse, TRequest, TProcessingResult>(EventProcessor<TResponse, TRequest, TProcessingResult> eventProcessor)
            where TResponse : IMessage
            where TRequest : IMessage
            where TProcessingResult : IProcessingResult
        {
            _reverseCallClientManager.Handle(
                eventProcessor.StreamingCall,
                eventProcessor.ResponseProperty,
                eventProcessor.RequestProperty,
                async (call) =>
                {
                    var requestProxy = eventProcessor.ProxyFromRequest(call.Request);
                    var executionContext = requestProxy.ExecutionContext;
                    _executionContextManager.CurrentFor(executionContext);

                    var response = await eventProcessor.ProcessRequest(call.Request).ConfigureAwait(false);

                    await call.Reply(response).ConfigureAwait(false);
                });
        }
    }
}