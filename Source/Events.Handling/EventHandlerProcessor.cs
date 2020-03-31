// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using contracts::Dolittle.Runtime.Events.Processing;
using Dolittle.Artifacts;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;
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
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventConverter _eventConverter;
        readonly IEventProcessingCompletion _eventHandlersWaiters;
        readonly IReverseCallClientManager _reverseCallClientManager;
        readonly IAsyncPolicyFor<EventHandlerProcessor> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessor"/> class.
        /// </summary>
        /// <param name="eventProcessingInvocationManager">The <see cref="IEventProcessingInvocationManager" />.</param>
        /// <param name="eventHandlersClient"><see cref="EventHandlersClient"/> for talking to the runtime.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for mapping types and artifacts.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager" />.</param>
        /// <param name="eventConverter"><see cref="IEventConverter"/> for converting events for transport.</param>
        /// <param name="eventHandlersWaiters"><see cref="IEventProcessingCompletion"/> for waiting on event handlers.</param>
        /// <param name="reverseCallClientManager">A <see cref="IReverseCallClientManager"/> for working with reverse calls from server.</param>
        /// <param name="policy"><see cref="IAsyncPolicyFor{T}"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerProcessor(
            IEventProcessingInvocationManager eventProcessingInvocationManager,
            EventHandlersClient eventHandlersClient,
            IArtifactTypeMap artifactTypeMap,
            IExecutionContextManager executionContextManager,
            IEventConverter eventConverter,
            IEventProcessingCompletion eventHandlersWaiters,
            IReverseCallClientManager reverseCallClientManager,
            IAsyncPolicyFor<EventHandlerProcessor> policy,
            ILogger logger)
        {
            _eventProcessingInvocationManager = eventProcessingInvocationManager;
            _eventHandlersClient = eventHandlersClient;
            _artifactTypeMap = artifactTypeMap;
            _executionContextManager = executionContextManager;
            _eventConverter = eventConverter;
            _eventHandlersWaiters = eventHandlersWaiters;
            _reverseCallClientManager = reverseCallClientManager;
            _policy = policy;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Start(AbstractEventHandler eventHandler, CancellationToken token)
        {
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

            var result = _eventHandlersClient.Connect(metadata, cancellationToken: token);
            return _reverseCallClientManager.Handle(
                result,
                _ => _.CallNumber,
                _ => _.CallNumber,
                async call =>
                {
                    var partition = call.Request.Partition.To<PartitionId>();
                    var executionContext = Execution.Contracts.ExecutionContext.Parser.ParseFrom(call.Request.ExecutionContext);
                    _executionContextManager.CurrentFor(executionContext);
                    var correlationId = executionContext.CorrelationId.To<CorrelationId>();

                    var committedEvent = _eventConverter.ToSDK(call.Request.Event);
                    var invocationManager = _eventProcessingInvocationManager.GetInvocationManagerFor<EventHandlerClientToRuntimeResponse, IProcessingResult>();
                    var response = await invocationManager.Invoke(
                        async () =>
                        {
                            await eventHandler.Invoke(committedEvent).ConfigureAwait(false);
                            return new SucceededProcessingResult();
                        },
                        eventHandler.Type,
                        partition,
                        call.Request.RetryProcessingState,
                        succeededProcessingResult => new EventHandlerClientToRuntimeResponse(),
                        response => response.Failed).ConfigureAwait(false);

                    try
                    {
                        _eventHandlersWaiters.EventHandlerCompletedForEvent(correlationId, eventHandler.Identifier, committedEvent.Event.GetType());
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning($"Error notifying waiters that event was processed : {correlationId} - {eventHandler.Identifier} : {ex.Message}");
                    }

                    await _policy.Execute(() => call.Reply(response)).ConfigureAwait(false);
                }, token);
        }

        void ThrowIfIllegalEventHandlerId(EventHandlerId id)
        {
            var stream = new StreamId { Value = id };
            if (stream.IsNonWriteable) throw new IllegalEventHandlerId(id);
        }
    }
}