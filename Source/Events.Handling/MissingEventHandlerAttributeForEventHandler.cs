// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="ICanHandleEvents">event handler</see> does not have the <see cref="EventHandlerAttribute"/>.
    /// </summary>
    public class MissingEventHandlerAttributeForEventHandler : MissingAttributeForEventHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingEventHandlerAttributeForEventHandler"/> class.
        /// </summary>
        /// <param name="eventHandlerType">Type of <see cref="ICanHandleEvents"/>.</param>
        public MissingEventHandlerAttributeForEventHandler(Type eventHandlerType)
            : base(eventHandlerType, typeof(EventHandlerAttribute), $"\"{Guid.Empty}\"")
        {
        }
    }
}