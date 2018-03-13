using System;
using Dolittle.Domain;
using Machine.Specifications;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class when_getting_a_stateless_aggregate_root_with_parameterless_constructor : given.a_command_context
    {
        protected static AggregateRootRepositoryFor<AggregateRootWithParameterlessConstructor> aggregate_root_repository;
        protected static Exception result;

        Establish context = () => aggregate_root_repository = new AggregateRootRepositoryFor<AggregateRootWithParameterlessConstructor>(command_context_manager.Object, event_store.Object, event_source_versions.Object, application_artifacts.Object, logger.Object);

        Because of = () => result = Catch.Exception(() => aggregate_root_repository.Get(Guid.NewGuid()));

        It should_throw_invalid_aggregate_root_constructor_signature = () => result.ShouldBeOfExactType<InvalidAggregateRootConstructorSignature>();
    }
}
