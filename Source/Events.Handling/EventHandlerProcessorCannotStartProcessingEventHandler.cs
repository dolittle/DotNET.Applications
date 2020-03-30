// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="IEventHandlerProcessor" /> cannot start processing an event handler.
    /// </summary>
    public class EventHandlerProcessorCannotStartProcessingEventHandler : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessorCannotStartProcessingEventHandler"/> class.
        /// </summary>
        /// <param name="eventHandlerProcessor">The <see cref="IEventHandlerProcessor" />.</param>
        /// <param name="eventHandler">The <see cref="AbstractEventHandler" />.</param>
        public EventHandlerProcessorCannotStartProcessingEventHandler(IEventHandlerProcessor eventHandlerProcessor, AbstractEventHandler eventHandler)
            : base($"The event handler processor '{eventHandlerProcessor.GetType().FullName}' cannot start processing event handler '{eventHandler.Identifier}' of type event handler type '{eventHandler.GetType().FullName}'")
        {
        }
    }
}