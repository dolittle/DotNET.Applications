// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Linq;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Artifacts;
using Dolittle.Events.Processing;
using Dolittle.Logging;
using Dolittle.Protobuf;
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
        readonly IEventProcessors _eventProcessors;
        readonly EventHandlersClient _eventHandlersClient;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessors">The <see cref="IEventProcessors" />.</param>
        /// <param name="eventHandlersClient"><see cref="EventHandlersClient"/> for talking to the runtime.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerProcessor(
            IEventProcessors eventProcessors,
            EventHandlersClient eventHandlersClient,
            IArtifactTypeMap artifactTypeMap,
            ILogger logger)
        {
            _eventProcessors = eventProcessors;
            _eventHandlersClient = eventHandlersClient;
            _artifactTypeMap = artifactTypeMap;
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
            _eventProcessors.RegisterAndStartProcessing<EventHandlerClientToRuntimeResponse, EventHandlerRuntimeToClientRequest, IProcessingResult>(
                ScopeId.Default,
                StreamId.AllStream,
                eventHandler.Identifier,
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                request => new EventHandlerProcessingRequestProxy(request),
                (result, request) => new EventHandlerProcessingResponseProxy(result, request),
                (failureReason, retry, retryTimeout) => retry ? new RetryProcessingResult(retryTimeout, failureReason) as IProcessingResult : new FailedProcessingResult(failureReason) as IProcessingResult,
                async @event =>
                {
                    await eventHandler.Invoke(@event).ConfigureAwait(false);
                    return new SucceededProcessingResult();
                });
        }

        void ThrowIfIllegalEventHandlerId(EventHandlerId id)
        {
            var stream = new StreamId { Value = id };
            if (stream.IsNonWriteable) throw new IllegalEventHandlerId(id);
        }
    }
}