// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ICanHandleEvents"/> does not have a method for a expected <see cref="IEvent"/>.
    /// </summary>
    public class MissingHandleMethodForEventType : EventHandlerCannotHandleEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingHandleMethodForEventType"/> class.
        /// </summary>
        /// <param name="handlerType">Type of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="eventType">Type of <see cref="IEvent"/>.</param>
        public MissingHandleMethodForEventType(Type handlerType, Type eventType)
            : base(handlerType, eventType, $"Event Handler does not have a handle method for the '{eventType.AssemblyQualifiedName}' event type.")
        {
        }
    }
}