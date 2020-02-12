// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a handler method for a specific <see cref="IEvent"/>.
    /// </summary>
    public interface IEventHandlerMethod
    {
        /// <summary>
        /// Gets the type of event the <see cref="EventHandlerMethod"/> is for.
        /// </summary>
        Type EventType { get; }

        /// <summary>
        /// Invoke the method.
        /// </summary>
        /// <param name="handler">The <see cref="ICanHandleEvents">handler</see> instance.</param>
        /// <param name="event"><see cref="CommittedEvent"/> to handle.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Invoke(ICanHandleEvents handler, CommittedEvent @event);
    }
}