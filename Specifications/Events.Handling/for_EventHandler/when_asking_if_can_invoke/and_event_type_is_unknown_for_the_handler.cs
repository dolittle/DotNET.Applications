// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Events.Handling.for_EventHandler.when_asking_if_can_invoke
{
    public class and_event_type_is_unknown_for_the_handler
    {
        static EventHandler event_handler;
        static bool result;

        Establish context = () => event_handler = new EventHandler(
                                                        Mock.Of<IContainer>(),
                                                        EventHandlerId.NotSet,
                                                        typeof(EventHandlerWithoutAnyHandleMethods),
                                                        Array.Empty<IEventHandlerMethod>());

        Because of = () => result = event_handler.CanInvoke(typeof(MyEvent));

        It should_not_be_able_to_invoke = () => result.ShouldBeFalse();
    }
}