// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;

namespace Dolittle.Events.Handling.for_EventHandlers.given
{
    public class an_event_handlers_instance
    {
        protected static Mock<IContainer> container;
        protected static Mock<IEventHandlerProcessor> event_handler_processor;
        protected static EventHandlers event_handlers;

        Establish context = () =>
        {
            container = new Mock<IContainer>();
            event_handler_processor = new Mock<IEventHandlerProcessor>();
            event_handlers = new EventHandlers(container.Object, event_handler_processor.Object);
        };
    }
}