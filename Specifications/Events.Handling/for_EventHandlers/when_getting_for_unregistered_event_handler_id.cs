// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Events.Handling.for_EventHandlers
{
    public class when_getting_for_unregistered_event_handler_id : given.an_event_handlers_instance
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => event_handlers.GetFor(Guid.NewGuid()));

        It should_throw_missing_event_handler_with_id = () => result.ShouldBeOfExactType<MissingEventHandlerWithId>();
    }
}