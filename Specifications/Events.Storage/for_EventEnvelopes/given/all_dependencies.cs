using Dolittle.Runtime.Events.Migration;
using Dolittle.Applications;
using Dolittle.Runtime.Execution;
using Dolittle.Time;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Events.Storage.Specs.for_EventEnvelopes.given
{
    public class all_dependencies
    {
        protected static Mock<IApplicationResources> application_resources;
        protected static Mock<ISystemClock> system_clock;
        protected static Mock<IExecutionContext> execution_context;
        protected static Mock<IEventMigrationHierarchyManager> event_migration_hierarchy_manager;

        Establish context = () =>
        {
            application_resources = new Mock<IApplicationResources>();
            system_clock = new Mock<ISystemClock>();
            execution_context = new Mock<IExecutionContext>();
            event_migration_hierarchy_manager = new Mock<IEventMigrationHierarchyManager>();
        };
    }
}
