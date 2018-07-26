using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Runtime.Events.Storage;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;
using Dolittle.Artifacts;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor.given
{
    public class all_dependencies
    {
        protected static Mock<ICommandContextManager> command_context_manager;
        protected static Mock<IEventSourceVersions> event_source_versions;
        protected static Mock<IEventStore> event_store;
        protected static Mock<IArtifactTypeMap> artifact_type_map;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            command_context_manager = new Mock<ICommandContextManager>();
            event_source_versions = new Mock<IEventSourceVersions>();
            event_store = new Mock<IEventStore>();
            artifact_type_map = new Mock<IArtifactTypeMap>();
            logger = new Mock<ILogger>();
        };
    }
}