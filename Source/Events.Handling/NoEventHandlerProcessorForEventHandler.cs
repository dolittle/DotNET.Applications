// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when there are no instances of <see cref="IEventHandlerProcessor" /> that can start processing an event handler.
    /// </summary>
    public class NoEventHandlerProcessorForEventHandler : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoEventHandlerProcessorForEventHandler"/> class.
        /// </summary>
        /// <param name="eventHandler">The <see cref="AbstractEventHandler" />.</param>
        public NoEventHandlerProcessorForEventHandler(AbstractEventHandler eventHandler)
            : base($"There are no instances of '{typeof(IEventHandlerProcessor).FullName}' that can start processing event handler '{eventHandler.Identifier}' of type '{eventHandler.Type.FullName}'")
        {
        }
    }
}