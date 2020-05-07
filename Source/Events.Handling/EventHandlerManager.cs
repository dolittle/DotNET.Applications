// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Events.Handling.Internal;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerManager"/> class.
        /// </summary>
        /// <param name="processors">The <see cref="EventHandlerProcessors"/> that will be used to create instances of <see cref="EventHandlerProcessor{TEventType}"/>.</param>
        /// <param name="policy">The <see cref="IAsyncPolicyFor{T}"/> that defines reconnect policies for event handlers.</param>
        public EventHandlerManager(
            EventHandlerProcessors processors,
            IAsyncPolicyFor<EventHandlerManager> policy)
        {
            _processors = processors;
            _policy = policy;
        }

        /// <inheritdoc/>
        public Task Register<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, IEventHandler<TEventType> handler, CancellationToken cancellationToken = default)
            where TEventType : IEvent
            => _processors.ProcessorFor(id, scope, partitioned, handler).RegisterAndHandleForeverWithPolicy(_policy, cancellationToken);
    }
}
