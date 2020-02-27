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
    public class and_event_type_is_unknown_for_the_handler
    {
        static EventHandler event_handler;
        static Exception result;

        static CommittedEvent committed_event;

        Establish context = () =>
        {
            committed_event = committed_events.single();

            event_handler = new EventHandler(
                                    Mock.Of<IContainer>(),
                                    EventHandlerId.NotSet,
                                    typeof(EventHandlerWithoutAnyHandleMethods),
                                    false,
                                    Array.Empty<IEventHandlerMethod>());
        };

        Because of = () => event_handler.Invoke(committed_event).ContinueWith(_ => result = _.Exception.InnerException).Wait();

        It should_throw_missing_handle_method_for_event_type = () => result.ShouldBeOfExactType<MissingHandleMethodForEventType>();
    }
}