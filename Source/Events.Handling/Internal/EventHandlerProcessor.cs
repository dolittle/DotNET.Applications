// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;

namespace Dolittle.Events.Handling.Internal
{
    /// <summary>
    /// Represents an event handler processor that wraps the gRPC protocol for event handlers.
    /// </summary>
    /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
    public class EventHandlerProcessor<TEventType>
        where TEventType : IEvent
    {
        readonly IEventConverter _converter;
        readonly ILogger _logger;

        public EventHandlerProcessor(IEventConverter converter, ILogger logger)
        {
            _converter = converter;
            _logger = logger;
        }

        /// <summary>
        /// Gets the potential <see cref="Failure"/> returned from the registration request.
        /// </summary>
        public Failure RegisterFailure { get; }

        /// <summary>
        /// Registers the event handler with the Runtime.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the event handler.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the event handler will run.</param>
        /// <param name="handler">The implementation of <see cref="ICanHandle{TEventType}"/> of type <typeparamref name="TEventType"/>.</param>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task" /> that, when resolved, returns whether a registration response was received.</returns>
        public Task<bool> Register(EventHandlerId id, ScopeId scope, ICanHandle<TEventType> handler, CancellationToken cancellationToken)
        {
        }

        /// <summary>
        /// Handles event handler request from the Runtime.
        /// </summary>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task Handle(CancellationToken cancellationToken)
        {
        }
    }
}