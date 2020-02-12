// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlers
{
    public class when_registering_type_which_is_not_event_handler : given.an_event_handlers_instance
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => event_handlers.Register(typeof(object), Guid.NewGuid()));

        It should_throw_type_is_not_an_event_andler = () => result.ShouldBeOfExactType<TypeIsNotAnEventHandler>();
    }
}