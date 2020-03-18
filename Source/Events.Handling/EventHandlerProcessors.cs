// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlerProcessors" />.
    /// </summary>
    public class EventHandlerProcessors : IEventHandlerProcessors
    {
        readonly IInstancesOf<IEventHandlerProcessor> _eventHandlerProcessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessors"/> class.
        /// </summary>
        /// <param name="eventHandlerProcessors">The <see cref="IInstancesOf{T}" /> of <see cref="IEventHandlerProcessor" />.</param>
        public EventHandlerProcessors(IInstancesOf<IEventHandlerProcessor> eventHandlerProcessors)
        {
           _eventHandlerProcessors = eventHandlerProcessors;
        }

        /// <inheritdoc/>
        public void Start(AbstractEventHandler eventHandler)
        {
            IEventHandlerProcessor processor = null;
            _eventHandlerProcessors.ForEach(_ =>
            {
                if (_.CanProcess(eventHandler))
                {
                    if (processor != null) throw new MultipleEventHandlerProcessorsForEventHandler(eventHandler);
                    processor = _;
                }
            });
            if (processor == null) throw new NoEventHandlerProcessorForEventHandler(eventHandler);
            processor.Start(eventHandler);
        }
    }
}