// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Linq;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Artifacts;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Services.Clients;
using Grpc.Core;
using static contracts::Dolittle.Runtime.Events.Processing.EventHandlers;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="EventHandlerProcessor"/>.
    /// </summary>
    public class EventHandlerProcessor : IEventHandlerProcessor
    {
        readonly IEventProcessingInvocationManager _eventProcessingInvocationManager;
        readonly EventHandlersClient _eventHandlersClient;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly IEventConverter _eventConverter;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessingInvocationManager">The <see cref="IEventProcessingInvocationManager" />.</param>
        /// <param name="eventHandlersClient"><see cref="EventHandlersClient"/> for talking to the runtime.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="reverseCallClientManager">The <see cref="IReverseCallClientManager" />.</param>
        /// <param name="eventConverter"><see cref="IEventConverter" />.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager" />.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerProcessor(
            IEventProcessingInvocationManager eventProcessingInvocationManager,
            EventHandlersClient eventHandlersClient,
            IArtifactTypeMap artifactTypeMap,
            IReverseCallClientManager reverseCallClientManager,
            IEventConverter eventConverter,
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _eventProcessingInvocationManager = eventProcessingInvocationManager;
            _eventHandlersClient = eventHandlersClient;
            _artifactTypeMap = artifactTypeMap;
            _reverseCallClientManager = reverseCallClientManager;
            _eventConverter = eventConverter;
            _executionContextManager = executionContextManager;
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
                Partitioned = eventHandler.Partitioned,
                Scope = eventHandler.Scope.ToProtobuf()
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
                async call =>
                {
                    var executionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(call.Request.ExecutionContext);
                    _executionContextManager.CurrentFor(executionContext);
                    var correlationId = executionContext.CorrelationId.To<CorrelationId>();

                    var committedEvent = _eventConverter.ToSDK(call.Request.Event);
                    await eventHandler.Invoke(committedEvent).ConfigureAwait(false);

                    try
                    {
                        _eventHandlersWaiters.EventHandlerCompletedForEvent(correlationId, eventHandler.Identifier, committedEvent.Event.GetType());
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning($"Error notifying waiters that event was processed : {correlationId} - {eventHandler.Identifier} : {ex.Message}");
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