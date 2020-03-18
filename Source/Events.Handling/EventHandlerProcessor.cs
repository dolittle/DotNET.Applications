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
using static contracts::Dolittle.Runtime.Events.Processing.EventHandlers;
using grpc = contracts::Dolittle.Runtime.Events.Processing;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="EventHandlerProcessor"/>.
    /// </summary>
    public class EventHandlerProcessor : IEventHandlerProcessor
    {
        readonly EventHandlersClient _eventHandlersClient;
        readonly IExecutionContextManager _executionContextManager;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IEventConverter _eventConverter;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="eventHandlersClient"><see cref="EventHandlersClient"/> for talking to the runtime.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing the <see cref="Execution.ExecutionContext"/>.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="eventConverter"><see cref="IEventConverter"/> for converting events for transport.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerProcessor(
            EventHandlersClient eventHandlersClient,
            IExecutionContextManager executionContextManager,
            IArtifactTypeMap artifactTypeMap,
            IEventConverter eventConverter,
            IReverseCallClientManager reverseCallClientManager,
            ILogger logger)
        {
            _eventHandlersClient = eventHandlersClient;
            _executionContextManager = executionContextManager;
            _artifactTypeMap = artifactTypeMap;
            _eventConverter = eventConverter;
            _reverseCallClientManager = reverseCallClientManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanProcess(AbstractEventHandler eventHandler) => eventHandler.GetType().Equals(typeof(EventHandler));

        /// <inheritdoc/>
        public void Start(AbstractEventHandler eventHandler)
        {
            if (!CanProcess(eventHandler)) throw new EventHandlerProcessorCannotStartProcessingEventHandler(this, eventHandler);
            ThrowIfIllegalEventHandlerId(eventHandler.Identifier);
            var artifacts = eventHandler.EventTypes.Select(_ => _artifactTypeMap.GetArtifactFor(_));
            var arguments = new EventHandlerArguments
            {
                EventHandler = eventHandler.Identifier.ToProtobuf(),
                Partitioned = eventHandler.Partitioned
            };
            arguments.Types_.AddRange(artifacts.Select(_ =>
                new grpcArtifacts.Artifact
                {
                    Id = _.Id.ToProtobuf(),
                    Generation = _.Generation
                }));

            var metadata = new Metadata { arguments.ToArgumentsMetadata() };

            _logger.Debug($"Connecting to runtime for event handler '{eventHandler.Identifier}' for types '{string.Join(",", artifacts)}', partioning: {arguments.Partitioned}");

            var result = _eventHandlersClient.Connect(metadata);
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

                        var response = new grpc.EventHandlerClientToRuntimeResponse
                        {
                            Succeeded = true,
                            Retry = false,
                            ExecutionContext = call.Request.ExecutionContext
                        };

                        var committedEvent = _eventConverter.ToSDK(call.Request.Event);
                        await eventHandler.Invoke(committedEvent).ConfigureAwait(false);

                        await call.Reply(response).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        var response = new grpc.EventHandlerClientToRuntimeResponse
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