using System;
using doLittle.Domain;
using Machine.Specifications;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class when_getting_a_stateless_aggregate_root_with_guid_in_constructor : given.a_command_context
    {
        protected static AggregateRootRepositoryFor<AggregateRootWithGuidConstructor> aggregate_root_repository;
        protected static AggregateRootWithGuidConstructor result;

        Establish context = () => aggregate_root_repository = new AggregateRootRepositoryFor<AggregateRootWithGuidConstructor>(command_context_manager.Object, event_store.Object, event_source_versions.Object, application_resources.Object, logger.Object);

        Because of = () => result = aggregate_root_repository.Get(Guid.NewGuid());

        It should_return_an_instance = () => result.ShouldNotBeNull();
    }
}
