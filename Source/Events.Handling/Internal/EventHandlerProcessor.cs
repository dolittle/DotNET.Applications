// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Artifacts;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Runtime.Events.Processing.Contracts;
using Dolittle.Services.Clients;
using Google.Protobuf.WellKnownTypes;
using static Dolittle.Runtime.Events.Processing.Contracts.EventHandlers;
using Artifact = Dolittle.Artifacts.Contracts.Artifact;

namespace Dolittle.Events.Handling.Internal
{
    /// <summary>
    /// Represents an event handler processor that wraps the gRPC protocol for event handlers.
    /// </summary>
    /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
    public class EventHandlerProcessor<TEventType>
        where TEventType : IEvent
    {
        readonly IReverseCallClient<EventHandlersClientToRuntimeMessage, EventHandlerRuntimeToClientMessage, EventHandlersRegistrationRequest, EventHandlerRegistrationResponse, HandleEventRequest, EventHandlerResponse> _client;
        readonly IEventProcessingCompletion _eventProcessingCompletion;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IEventConverter _converter;
        readonly ILogger _logger;
        EventHandlerId _id;
        IEventHandler<TEventType> _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessor{TEventType}"/> class.
        /// </summary>
        /// <param name="handlersClient">The <see cref="EventHandlersClient"/> to use to connect to the Runtime.</param>
        /// <param name="reverseCallClients">The <see cref="IReverseCallClients"/> to use for creating instances of <see cref="IReverseCallClient{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.</param>
        /// <param name="eventProcessingCompletion">The <see cref="IEventProcessingCompletion"/> to use for notifying of event handling completion.</param>
        /// <param name="artifactTypeMap">The <see cref="IArtifactTypeMap"/> to use for converting event types to artifacts.</param>
        /// <param name="converter">The <see cref="IEventConverter"/> to use to convert events.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public EventHandlerProcessor(
            EventHandlersClient handlersClient,
            IReverseCallClients reverseCallClients,
            IEventProcessingCompletion eventProcessingCompletion,
            IArtifactTypeMap artifactTypeMap,
            IEventConverter converter,
            ILogger logger)
        {
            _client = reverseCallClients.GetFor<EventHandlersClientToRuntimeMessage, EventHandlerRuntimeToClientMessage, EventHandlersRegistrationRequest, EventHandlerRegistrationResponse, HandleEventRequest, EventHandlerResponse>(
                () => handlersClient.Connect(),
                (message, arguments) => message.RegistrationRequest = arguments,
                message => message.RegistrationResponse,
                message => message.HandleRequest,
                (message, response) => message.HandleResult = response,
                (arguments, context) => arguments.CallContext = context,
                request => request.CallContext,
                (response, context) => response.CallContext = context);
            _eventProcessingCompletion = eventProcessingCompletion;
            _artifactTypeMap = artifactTypeMap;
            _converter = converter;
            _logger = logger;
        }

        /// <summary>
        /// Gets the potential <see cref="Failure"/> returned from the registration request.
        /// </summary>
        public Failure RegisterFailure => _client.ConnectResponse.Failure;

        /// <summary>
        /// Registers the event handler with the Runtime.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the event handler.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the event handler will run.</param>
        /// <param name="partitioned">Whether the event handler should create a partitioned stream or not.</param>
        /// <param name="handler">The event handler.</param>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task"/> that, when resolved, returns whether a registration response was received.</returns>
        public Task<bool> Register(EventHandlerId id, ScopeId scope, bool partitioned, IEventHandler<TEventType> handler, CancellationToken cancellationToken)
        {
            _id = id;
            _handler = handler;

            var arguments = new EventHandlersRegistrationRequest
            {
                EventHandlerId = id.ToProtobuf(),
                ScopeId = scope.ToProtobuf(),
                Partitioned = partitioned,
            };

            foreach (var eventType in handler.HandledEventTypes)
            {
                var artifact = _artifactTypeMap.GetArtifactFor(eventType);
                arguments.Types_.Add(new Artifact
                {
                    Id = artifact.Id.ToProtobuf(),
                    Generation = artifact.Generation,
                });
            }

            return _client.Connect(arguments, cancellationToken);
        }

        /// <summary>
        /// Handles event handler request from the Runtime.
        /// </summary>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task Handle(CancellationToken cancellationToken)
            => _client.Handle(Call, cancellationToken);

        async Task<EventHandlerResponse> Call(HandleEventRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var comitted = _converter.ToSDK(request.Event.Event);
                if (comitted.Event is TEventType typedEvent)
                {
                    var context = new EventContext(comitted.EventSource, comitted.Occurred);
                    await _handler.Handle(typedEvent, context).ConfigureAwait(false);
                    return new EventHandlerResponse();
                }

                throw new EventHandlerDoesNotHandleEvent(typeof(TEventType));
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;

                _logger.Warning(ex, "Error while invoking event handler. Will retry later.");

                return new EventHandlerResponse
                {
                    Failure = new ProcessorFailure
                    {
                        Reason = ex.Message,
                        Retry = true,
                        RetryTimeout = Duration.FromTimeSpan(TimeSpan.FromSeconds(Math.Min(request.RetryProcessingState.RetryCount * 5, 60))),
                    },
                };
            }
            finally
            {
                try
                {
                    var comitted = _converter.ToSDK(request.Event.Event);
                    _eventProcessingCompletion.EventHandlerCompletedForEvent(comitted.ExecutionContext.CorrelationId, _id, comitted.Event.GetType());
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex, "Error notifying waiters of event handler completion");
                }
            }
        }
    }
}