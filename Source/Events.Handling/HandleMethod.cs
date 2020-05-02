// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents a ".Handle(event, context)" method on an event handler.
    /// </summary>
    /// <param name="event">The <typeparamref name="TEventType"/> to handle.</param>
    /// <param name="context">The <see cref="EventContext"/> of the event to handle.</param>
    /// <typeparam name="TEventType">The event type that the event handler can handle.</typeparam>
    /// <returns>A <see cref="Task"/> representing the asyncronous operation.</returns>
    public delegate Task HandleMethod<TEventType>(TEventType @event, EventContext context)
        where TEventType : IEvent;
}
