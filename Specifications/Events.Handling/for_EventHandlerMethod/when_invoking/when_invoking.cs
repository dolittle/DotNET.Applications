// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlerMethod.when_invoking
{
    public class when_invoking
    {
        static EventHandlerMethod method;
        static CorrectEventHandler event_handler;
        static CommittedEvent committed_event;
        static EventContext event_context;

        Establish context = () =>
        {
            Expression<Action<CorrectEventHandler>> expression = (CorrectEventHandler _) => _.Handle(null, null);
            method = new EventHandlerMethod(typeof(MyEvent), expression.GetMethodInfo());
            event_handler = new CorrectEventHandler();
            committed_event = committed_events.single();
            event_context = committed_event.DeriveContext();
        };

        Because of = () => method.Invoke(event_handler, committed_event).GetAwaiter().GetResult();

        It should_invoke_the_handle_method_with_the_correct_event = () => event_handler.EventPassed.ShouldEqual(committed_event.Event);
        It should_invoke_the_handle_method_with_the_correct_event_source = () => event_handler.EventContextPassed.EventSourceId.ShouldEqual(committed_event.DeriveContext().EventSourceId);
        It should_invoke_the_handle_method_with_the_correct_occurred_at = () => event_handler.EventContextPassed.Occurred.ShouldEqual(committed_event.DeriveContext().Occurred);
    }
}