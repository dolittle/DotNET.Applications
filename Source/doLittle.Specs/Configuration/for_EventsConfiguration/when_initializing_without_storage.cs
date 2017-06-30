using System;
using doLittle.Configuration;
using doLittle.Entities;
using doLittle.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Configuration.for_EventsConfiguration
{
    [Subject(typeof(EventsConfiguration))]
    public class when_initializing_without_storage : given.an_events_configuration_and_container_object
    {
        Because of = () => events_configuration.Initialize(container.Object);

        It should_be_initialized = () => events_configuration.ShouldNotBeNull();
        It should_not_set_up_storage_for_events = () => container.Verify(c => c.Bind(typeof(IEntityContext<IEvent>), Moq.It.IsAny<Type>()), Times.Never());
    }
}
