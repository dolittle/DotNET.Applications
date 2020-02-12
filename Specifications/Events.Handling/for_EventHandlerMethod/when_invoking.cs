// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlerMethod
{
    public class when_invoking
    {
        static EventHandlerMethod method;
        static MyEventHandler event_handler;
        static CommittedEvent committed_event;

        Establish context = () =>
        {
            Expression<Action<MyEventHandler>> expression = (MyEventHandler _) => _.AsyncHandle(null);
            method = new EventHandlerMethod(typeof(MyEvent), expression.GetMethodInfo());
            event_handler = new MyEventHandler();
            committed_event = new CommittedEvent(new MyEvent(), DateTimeOffset.UtcNow);
        };

        Because of = () => method.Invoke(event_handler, committed_event).Wait();

        It should_invoke_the_handle_method = () => event_handler.EventPassed.ShouldEqual(committed_event.Event);
    }
}