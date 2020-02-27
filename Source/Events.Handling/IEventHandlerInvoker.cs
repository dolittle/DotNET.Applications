// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that manages the invocation of an <see cref="EventHandler" />. 
    /// </summary>
    public interface IEventHandlerInvoker
    {
        /// <summary>
        /// Invokes the Event Handler Method with the <see cref="CommittedEvent" />.
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        Task InvokeOn(EventHandler eventHandler, CommittedEvent @event);
    }
}