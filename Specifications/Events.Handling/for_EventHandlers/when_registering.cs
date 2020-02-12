// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Events.Handling.for_EventHandlers
{
    public class when_registering : given.an_event_handlers_instance
    {
        static EventHandler result;
        static EventHandlerId event_handler_id = Guid.NewGuid();

        Because of = () => result = event_handlers.Register(typeof(MyEventHandler), event_handler_id);

        It should_return_it_when_getting_it = () => event_handlers.GetFor(event_handler_id).ShouldNotBeNull();
        It should_consider_having_it_when_asked_if_it_has = () => event_handlers.HasFor(event_handler_id).ShouldBeTrue();
        It should_have_two_events_it_supports = () => result.EventTypes.ShouldContainOnly(typeof(MyFirstEvent), typeof(MySecondEvent));
        It should_start_processing_for_the_handler = () => event_handler_processor.Verify(_ => _.Start(Moq.It.IsAny<EventHandler>()), Times.Once);
    }
}