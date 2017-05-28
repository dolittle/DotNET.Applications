using doLittle.Specs.Events.Fakes;
using doLittle.Specs.Events.for_EventMigrationManager.given;
using Machine.Specifications;

namespace doLittle.Specs.Events.for_EventMigrationService.given
{
    public abstract class an_event_with_a_migrator : an_event_migrator_service_with_no_registered_migrators
    {
        Establish context = () => event_migrator_manager.RegisterMigrator(typeof(SimpleEventV1ToV2Migrator));
    }
}