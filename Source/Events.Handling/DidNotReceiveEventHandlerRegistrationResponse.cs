// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when no event handler registration response is recieved after registering an event handler.
    /// </summary>
    public class DidNotReceiveEventHandlerRegistrationResponse : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DidNotReceiveEventHandlerRegistrationResponse"/> class.
        /// </summary>
        /// <param name="id">The unique <see cref="EventHandlerId"/> for the event handler.</param>
        public DidNotReceiveEventHandlerRegistrationResponse(EventHandlerId id)
            : base($"Did not receive event handler registration response while registering event handler {id}")
        {
        }
    }
}