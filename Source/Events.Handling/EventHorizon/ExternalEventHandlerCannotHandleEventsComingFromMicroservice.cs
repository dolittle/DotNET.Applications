// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Applications;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when <see cref="ExternalEventHandler" /> cannot handle event because the <see cref="Microservice" /> came from does not correspond with the <see cref="Microservice" />
    /// that this <see cref="ExternalEventHandler" /> can handle events from.
    /// </summary>
    public class ExternalEventHandlerCannotHandleEventsComingFromMicroservice : EventHandlerCannotHandleEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerCannotHandleEventsComingFromMicroservice"/> class.
        /// </summary>
        /// <param name="eventHandler">The <see cref="ExternalEventHandler" />.</param>
        /// <param name="event">The <see cref="CommittedEvent" />.</param>
        public ExternalEventHandlerCannotHandleEventsComingFromMicroservice(ExternalEventHandler eventHandler, CommittedEvent @event)
            : base(eventHandler.GetType(), @event.GetType(), $"Event came from microservice '{@event.Microservice}' but event handler can only handle events from microservice '{eventHandler.Microservice}'")
        {
        }
    }
}