using doLittle.Applications;
using doLittle.Commands;
using doLittle.Events;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Domain.for_AggregateRootRepository.given
{
    public class all_dependencies
    {
        protected static Mock<ICommandContextManager> command_context_manager;
        protected static Mock<IEventSourceVersions> event_source_versions;
        protected static Mock<IEventStore> event_store;
        protected static Mock<IApplicationResources> application_resources;

        Establish context = () =>
        {
            command_context_manager = new Mock<ICommandContextManager>();
            event_source_versions = new Mock<IEventSourceVersions>();
            event_store = new Mock<IEventStore>();
            application_resources = new Mock<IApplicationResources>();
        };
    }
}
