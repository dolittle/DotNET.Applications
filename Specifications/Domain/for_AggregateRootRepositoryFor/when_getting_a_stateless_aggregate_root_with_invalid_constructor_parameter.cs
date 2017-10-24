using System;
using doLittle.Domain;
using Machine.Specifications;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class when_getting_a_stateless_aggregate_root_with_invalid_constructor_parameter : given.a_command_context
    {
        protected static AggregateRootRepositoryFor<AggregateRootWithInvalidConstructorParameter> aggregate_root_repository;
        protected static Exception result;

        Establish context = () => aggregate_root_repository = new AggregateRootRepositoryFor<AggregateRootWithInvalidConstructorParameter>(command_context_manager.Object, event_store.Object, event_source_versions.Object, application_resources.Object, logger.Object);

        Because of = () => result = Catch.Exception(() => aggregate_root_repository.Get(Guid.NewGuid()));

        It should_throw_invalid_aggregate_root_constructor_signature = () => result.ShouldBeOfExactType<InvalidAggregateRootConstructorSignature>();
    }
}
