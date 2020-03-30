// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an event handler is invoked to handle an event that it cannot handle.
    /// </summary>
    public class EventHandlerCannotHandleEvent : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerCannotHandleEvent"/> class.
        /// </summary>
        /// <param name="eventHandlerType">The <see cref="Type" /> of the event handler.</param>
        /// <param name="eventType">The <see cref="Type" /> of the event that could not be handled.</param>
        /// <param name="reason">The <see cref="EventHandlerCannotHandleEventReason" />.</param>
        public EventHandlerCannotHandleEvent(Type eventHandlerType, Type eventType, EventHandlerCannotHandleEventReason reason)
            : base($"Event Handler '{eventHandlerType.AssemblyQualifiedName}' cannot handle event '{eventType.AssemblyQualifiedName}'. {reason}")
        {
        }
    }
}