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
using static contracts::Dolittle.Runtime.Events.Processing.ScopedEventHandlers;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="EventHandlerProcessor"/>.
    /// </summary>
    public class ExternalEventHandlerProcessor : IEventHandlerProcessor
    {
        readonly IEventProcessors _eventProcessors;
        readonly ScopedEventHandlersClient _scopedEventHandlersClient;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessors">The <see cref="IEventProcessors" />.</param>
        /// <param name="scopedEventHandlersClient"><see cref="ScopedEventHandlersClient" /> for talking to the runtime.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ExternalEventHandlerProcessor(
            IEventProcessors eventProcessors,
            ScopedEventHandlersClient scopedEventHandlersClient,
            IArtifactTypeMap artifactTypeMap,
            ILogger logger)
        {
            _eventProcessors = eventProcessors;
            _scopedEventHandlersClient = scopedEventHandlersClient;
            _artifactTypeMap = artifactTypeMap;
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
            _eventProcessors.RegisterAndStartProcessing<ScopedEventHandlerClientToRuntimeResponse, ScopedEventHandlerRuntimeToClientRequest, IProcessingResult>(
                externalEventHandler.Scope,
                StreamId.AllStream,
                externalEventHandler.Identifier,
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                request => new ExternalEventHandlerProcessingRequestProxy(request),
                (result, request) => new ExternalEventHandlerProcessingResponseProxy(result, request),
                (failureReason, retry, retryTimeout) => retry ? new RetryProcessingResult(retryTimeout, failureReason) as IProcessingResult : new FailedProcessingResult(failureReason) as IProcessingResult,
                async @event =>
                {
                    await externalEventHandler.Invoke(@event).ConfigureAwait(false);
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