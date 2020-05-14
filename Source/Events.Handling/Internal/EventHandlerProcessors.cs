// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Artifacts;
using Dolittle.Logging;
using Dolittle.Services.Clients;
using static Dolittle.Runtime.Events.Processing.Contracts.EventHandlers;

namespace Dolittle.Events.Handling.Internal
{
    /// <summary>
    /// A class used to construct instances of <see cref="EventHandlerProcessor{TEventType}"/>.
    /// </summary>
    public class EventHandlerProcessors
    {
        readonly EventHandlersClient _handlersClient;
        readonly IReverseCallClients _reverseCallClients;
        readonly IEventProcessingCompletion _eventProcessingCompletion;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly IEventConverter _converter;
        readonly ILoggerManager _loggerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessors"/> class.
        /// </summary>
        /// <param name="handlersClient">The <see cref="EventHandlersClient"/> to use to connect to the Runtime.</param>
        /// <param name="reverseCallClients">The <see cref="IReverseCallClients"/> to use for creating instances of <see cref="IReverseCallClient{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.</param>
        /// <param name="eventProcessingCompletion">The <see cref="IEventProcessingCompletion"/> to use for notifying of event handling completion.</param>
        /// <param name="artifactTypeMap">The <see cref="IArtifactTypeMap"/> to use for converting event types to artifacts.</param>
        /// <param name="converter">The <see cref="IEventConverter"/> to use to convert events.</param>
        /// <param name="loggerManager">The <see cref="ILoggerManager"/> to use for creating instances of <see cref="ILogger"/>.</param>
        public EventHandlerProcessors(
            EventHandlersClient handlersClient,
            IReverseCallClients reverseCallClients,
            IEventProcessingCompletion eventProcessingCompletion,
            IArtifactTypeMap artifactTypeMap,
            IEventConverter converter,
            ILoggerManager loggerManager)
        {
            _handlersClient = handlersClient;
            _reverseCallClients = reverseCallClients;
            _eventProcessingCompletion = eventProcessingCompletion;
            _artifactTypeMap = artifactTypeMap;
            _converter = converter;
            _loggerManager = loggerManager;
        }

        /// <summary>
        /// Creates an <see cref="EventHandlerProcessor{TEventType}"/> of type <typeparamref name="TEventType"/>.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the event handler.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the event handler will run.</param>
        /// <param name="partitioned">Whether the event handler should create a partitioned stream or not.</param>
        /// <param name="handler">The <see cref="IEventHandler{TEventType}"/> that will be called to handle incoming events.</param>
        /// <typeparam name="TEventType">The type of events to process.</typeparam>
        /// <returns>An <see cref="EventHandlerProcessor{TEventType}"/> of type <typeparamref name="TEventType"/>.</returns>
        public EventHandlerProcessor<TEventType> GetFor<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, IEventHandler<TEventType> handler)
            where TEventType : IEvent
            => new EventHandlerProcessor<TEventType>(
                id,
                scope,
                partitioned,
                handler,
                _handlersClient,
                _reverseCallClients,
                _eventProcessingCompletion,
                _artifactTypeMap,
                _converter,
                _loggerManager.CreateLogger<EventHandlerProcessor<TEventType>>());
    }
}