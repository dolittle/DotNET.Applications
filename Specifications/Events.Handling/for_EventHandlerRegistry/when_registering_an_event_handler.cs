// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Events.Handling.Internal;
using Dolittle.Resilience;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Events.Handling.for_EventHandlerRegistry
{
    public class when_registering_an_event_handler
    {
        static Mock<EventHandlerProcessors> processors;
        static Mock<IAsyncPolicyFor<EventHandlerRegistry>> policy;

        static Mock<IEventProcessingCompletion> completion;

        static Mock<IEventHandler<IEvent>> handler;

        static EventHandlerRegistry registry;

        Establish context = () =>
        {
            processors = new Mock<EventHandlerProcessors>();
            policy = new Mock<IAsyncPolicyFor<EventHandlerRegistry>>();
            completion = new Mock<IEventProcessingCompletion>();
            handler = new Mock<IEventHandler<IEvent>>();
            registry = new EventHandlerRegistry(processors.Object, policy.Object, completion.Object);
        };

        Because of = () => registry.Register<IEvent>(Guid.Empty, Guid.Empty, false, handler.Object);

        It should_register_the_handler_to_the_completion = () => completion.Verify(_ => _.RegisterHandler(Guid.Empty, handler.Object.HandledEventTypes));
    }
}
