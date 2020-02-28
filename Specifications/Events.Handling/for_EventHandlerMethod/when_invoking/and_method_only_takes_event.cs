// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlerMethod.when_invoking
{
    public class and_method_only_takes_event
    {
        static EventHandlerMethod method;
        static EventHandlerWithOnlyEventParameter event_handler;
        static CommittedEvent committed_event;
        static Exception exception;

        Establish context = () =>
        {
            Expression<Action<EventHandlerWithOnlyEventParameter>> expression = (EventHandlerWithOnlyEventParameter _) => _.Handle(null);
            method = new EventHandlerMethod(typeof(MyEvent), expression.GetMethodInfo());
            event_handler = new EventHandlerWithOnlyEventParameter();
            committed_event = committed_events.single();
        };

        Because of = () => exception = Catch.Exception(() => method.Invoke(event_handler, committed_event).GetAwaiter().GetResult());

        It should_fail_because_signature_does_not_take_two_parameters = () => exception.ShouldBeOfExactType<EventHandlerMethodMustTakeTwoParameters>();
    }
}