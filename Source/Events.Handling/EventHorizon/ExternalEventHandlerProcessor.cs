// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Services.Clients;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.ScopedEventHandlers;
using grpc = contracts::Dolittle.Runtime.Events.Processing;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="EventHandlerProcessor"/>.
    /// </summary>
    public class ExternalEventHandlerProcessor : IEventHandlerProcessor
    {
        readonly ScopedEventHandlersClient _scopedEventHandlersClient;
        readonly IExecutionContextManager _executionContextManager;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IEventConverter _eventConverter;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="scopedEventHandlersClient"><see cref="ScopedEventHandlersClient" /> for talking to the runtime.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing the <see cref="Execution.ExecutionContext"/>.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="eventConverter"><see cref="IEventConverter"/> for converting events for transport.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ExternalEventHandlerProcessor(
            ScopedEventHandlersClient scopedEventHandlersClient,
            IExecutionContextManager executionContextManager,
            IArtifactTypeMap artifactTypeMap,
            IEventConverter eventConverter,
            IReverseCallClientManager reverseCallClientManager,
            ILogger logger)
        {
            _scopedEventHandlersClient = scopedEventHandlersClient;
            _executionContextManager = executionContextManager;
            _artifactTypeMap = artifactTypeMap;
            _eventConverter = eventConverter;
            _reverseCallClientManager = reverseCallClientManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanProcess(AbstractEventHandler eventHandler) => eventHandler.GetType().Equals(typeof(ExternalEventHandler));

        /// <inheritdoc/>
        public void Start(AbstractEventHandler eventHandler)
        {
            if (!CanProcess(eventHandler)) throw new EventHandlerProcessorCannotStartProcessingEventHandler(this, eventHandler);
            var externalEventHandler = eventHandler as ExternalEventHandler;
            ThrowIfIllegalEventHandlerId(eventHandler.Identifier);
            var artifacts = externalEventHandler.EventTypes.Select(_ => _artifactTypeMap.GetArtifactFor(_));
            var arguments = new ScopedEventHandlerArguments
            {
                EventHandler = externalEventHandler.Identifier.ToProtobuf(),
                Partitioned = externalEventHandler.Partitioned,
                Scope = externalEventHandler.Scope.ToProtobuf()
            };
            arguments.Types_.AddRange(artifacts.Select(_ =>
                new grpcArtifacts.Artifact
                {
                    Id = _.Id.ToProtobuf(),
                    Generation = _.Generation
                }));

            var metadata = new Metadata { arguments.ToArgumentsMetadata() };

            _logger.Debug($"Connecting to runtime for external event handler '{externalEventHandler.Identifier}' for scope '{externalEventHandler.Scope}', types '{string.Join(",", artifacts)}', partioning: {arguments.Partitioned}");

            var result = _scopedEventHandlersClient.Connect(metadata);
            _reverseCallClientManager.Handle(
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                async (call) =>
                {
                    try
                    {
                        var executionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(call.Request.ExecutionContext);
                        _executionContextManager.CurrentFor(executionContext);

                        var response = new grpc.ScopedEventHandlerClientToRuntimeResponse
                        {
                            Succeeded = true,
                            Retry = false,
                            ExecutionContext = call.Request.ExecutionContext
                        };

                        var committedEvent = _eventConverter.ToSDK(call.Request.Event);
                        await externalEventHandler.Invoke(committedEvent).ConfigureAwait(false);
                        await call.Reply(response).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        var response = new grpc.ScopedEventHandlerClientToRuntimeResponse
                        {
                            Succeeded = false,
                            Retry = false,
                            FailureReason = $"Failure Message: {ex.Message}\nStack Trace: {ex.StackTrace}",
                            ExecutionContext = call.Request.ExecutionContext
                        };
                        await call.Reply(response).ConfigureAwait(false);

                        _logger.Error(ex, "Error handling event");
                    }
                });
        }

        void ThrowIfIllegalEventHandlerId(EventHandlerId id)
        {
            var stream = new StreamId { Value = id };
            if (stream.IsNonWriteable) throw new IllegalEventHandlerId(id);
        }
    }
}