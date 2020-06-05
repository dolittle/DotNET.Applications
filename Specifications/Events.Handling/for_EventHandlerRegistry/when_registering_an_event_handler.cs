// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Events.Handling.Internal;
using Dolittle.Events.Processing.Internal;
using Dolittle.Resilience;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using MIt = Moq.It;

namespace Dolittle.Events.Handling.for_EventHandlerRegistry
{
    public class when_registering_an_event_handler
    {
        static Mock<IEventProcessingCompletion> _completion;

        static Mock<IEventHandler<IEvent>> _handler;

        static EventHandlerRegistry registry;

        Establish context = () =>
        {
            var policy = new Mock<IAsyncPolicyFor<EventHandlerRegistry>>();
            _completion = new Mock<IEventProcessingCompletion>();
            _handler = new Mock<IEventHandler<IEvent>>();

            var processors = new Mock<IEventHandlerProcessors>();
            processors.Setup(_ => _.GetFor<IEvent>(
                MIt.IsAny<EventHandlerId>(),
                MIt.IsAny<ScopeId>(),
                MIt.IsAny<bool>(),
                MIt.IsAny<IEventHandler<IEvent>>()))
                .Returns(new Mock<IEventProcessor>().Object);
            registry = new EventHandlerRegistry(processors.Object, policy.Object, _completion.Object);
        };

        Because of = () => registry.Register<IEvent>(Guid.Empty, Guid.Empty, false, _handler.Object);

        It should_register_the_handler_to_the_completion = () =>
            _completion.Verify(_ => _.RegisterHandler(Guid.Empty, _handler.Object.HandledEventTypes), Times.Once());
    }
}
