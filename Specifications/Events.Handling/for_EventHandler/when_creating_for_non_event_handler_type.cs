// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Events.Handling.for_EventHandler.when_asking_if_can_invoke
{
    public class when_creating_for_non_event_handler_type
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => new EventHandler(Mock.Of<IContainer>(), EventHandlerId.NotSet, typeof(object), Array.Empty<IEventHandlerMethod>()));

        It should_throw_type_is_not_an_event_handler = () => result.ShouldBeOfExactType<TypeIsNotAnEventHandler>();
    }
}