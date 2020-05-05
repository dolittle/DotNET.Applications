// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Events.Handling.Internal;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a manager that deals with registering event handlers with the Runtime.
    /// </summary>
    public interface IEventHandlerManager
    {
        /// <summary>
        /// Registers an event handler with the Runtime.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the handler.</param>
        /// <param name="scope">The <see cref="ScopeId"/> of the scope in the Event Store where the handler will run.</param>
        /// <param name="partitioned">Whether the event handler produces a partitioned stream or not.</param>
        /// <param name="cancellationToken">Token that can be used to cancel this operation.</param>
        /// <typeparam name="THandlerType">The type of the event handler.</typeparam>
        /// <typeparam name="TEventType">The event type that the event handler can handle.</typeparam>
        /// <returns>A <see cref="Task"/> representing the execution of the event handler.</returns>
        Task Register<THandlerType, TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, CancellationToken cancellationToken = default)
            where THandlerType : ICanHandle<TEventType>
            where TEventType : IEvent;
    }
}
