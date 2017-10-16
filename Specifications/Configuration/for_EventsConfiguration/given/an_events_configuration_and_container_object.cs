using doLittle.Configuration;
using doLittle.DependencyInversion;
using doLittle.Execution;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Configuration.for_EventsConfiguration.given
{
    public class an_events_configuration_and_container_object : an_events_configuration
    {
        protected static Mock<IContainer> container;

        Establish context = () => container = new Mock<IContainer>();
    }
}