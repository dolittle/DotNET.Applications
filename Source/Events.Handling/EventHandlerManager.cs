// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Events.Handling.Internal;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// An implementation of <see cref="IEventHandlerManager"/>.
    /// </summary>
    public class EventHandlerManager : IEventHandlerManager
    {
        readonly EventHandlerProcessors _processors;
        readonly IAsyncPolicyFor<EventHandlerManager> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerManager"/> class.
        /// </summary>
        /// <param name="processors">The <see cref="EventHandlerProcessors"/> that will be used to create instances of <see cref="EventHandlerProcessor{TEventType}"/>.</param>
        /// <param name="policy">The <see cref="IAsyncPolicyFor{T}"/> that defines reconnect policies for event handlers.</param>
        /// <param name="logger">The <see cref="ILogger"/> used for logging.</param>
        public EventHandlerManager(
            EventHandlerProcessors processors,
            IAsyncPolicyFor<EventHandlerManager> policy,
            ILogger logger)
        {
            _processors = processors;
            _policy = policy;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Register<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, IEventHandler<TEventType> handler, CancellationToken cancellationToken = default)
            where TEventType : IEvent
            => Start(id, scope, partitioned, _processors.ProcessorFor<TEventType>(), handler, cancellationToken);

        Task Start<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, EventHandlerProcessor<TEventType> processor, IEventHandler<TEventType> handler, CancellationToken cancellationToken)
            where TEventType : IEvent
            => _policy.Execute(
                async (cancellationToken) =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var receivedResponse = await processor.Register(id, scope, partitioned, handler, cancellationToken).ConfigureAwait(false);
                        ThrowIfNotReceivedResponse(id, receivedResponse);
                        ThrowIfRegisterFailure(id, processor.RegisterFailure);
                        await processor.Handle(cancellationToken).ConfigureAwait(false);
                    }
                },
                cancellationToken);

        void ThrowIfNotReceivedResponse(EventHandlerId id, bool receivedResponse)
        {
            if (!receivedResponse) throw new DidNotReceiveEventHandlerRegistrationResponse(id);
        }

        void ThrowIfRegisterFailure(EventHandlerId id, Failure registerFailure)
        {
            if (registerFailure != null) throw new EventHandlerRegistrationFailed(id, registerFailure);
        }
    }
}
