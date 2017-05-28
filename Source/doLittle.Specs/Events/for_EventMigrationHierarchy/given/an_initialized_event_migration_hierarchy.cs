using doLittle.Events;
using doLittle.Specs.Events.Fakes;
using Machine.Specifications;
using System;

namespace doLittle.Specs.Events.for_EventMigrationHierarchy.given
{
    public class an_initialized_event_migration_hierarchy
    {
        protected static Type hierarchy_for_type;
        protected static EventMigrationHierarchy event_migration_hierarchy;

        private Establish context = () =>
                                        {
                                            hierarchy_for_type = typeof (SimpleEvent);
                                            event_migration_hierarchy = new EventMigrationHierarchy(hierarchy_for_type);
                                        };
    }
}