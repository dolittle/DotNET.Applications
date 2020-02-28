// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA2008

namespace Dolittle.Events.Handling.for_EventHandler.when_invoking
{
    public class and_event_type_is_known_for_the_handler
    {
        static Mock<IContainer> container;
        static Mock<IEventHandlerMethod> event_handler_method;
        static EventHandler event_handler;
        static Exception result;

        static CommittedEvent committed_event;

        static EventHandlerWithoutAnyHandleMethods handler;

        Establish context = () =>
        {
            committed_event = committed_events.single();

            event_handler_method = new Mock<IEventHandlerMethod>();
            event_handler_method.SetupGet(_ => _.EventType).Returns(typeof(MyEvent));
            container = new Mock<IContainer>();
            handler = new EventHandlerWithoutAnyHandleMethods();
            container.Setup(_ => _.Get(typeof(EventHandlerWithoutAnyHandleMethods))).Returns(handler);

            event_handler = new EventHandler(
                                    container.Object,
                                    EventHandlerId.NotSet,
                                    typeof(EventHandlerWithoutAnyHandleMethods),
                                    false,
                                    new[] { event_handler_method.Object });
        };

        Because of = () => event_handler.Invoke(committed_event).Wait();

        It should_invoke_event_handler_method = () => event_handler_method.Verify(_ => _.Invoke(handler, committed_event), Times.Once());
    }
}