// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlerMethod
{
    public class when_creating_for_async_void_method
    {
        static Exception result;

        Because of = () =>
        {
            Expression<Action<MyEventHandler>> expression = (MyEventHandler _) => _.AsyncVoidHandle(null);
            result = Catch.Exception(() => new EventHandlerMethod(typeof(MyEvent), expression.GetMethodInfo()));
        };

        It should_throw_event_handler_method_must_be_asynchronous = () => result.ShouldBeOfExactType<EventHandlerMethodMustBeAsynchronous>();
    }
}