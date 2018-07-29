using Dolittle.Applications;
using Dolittle.Domain;
using Dolittle.Runtime.Commands.Coordination;
using Machine.Specifications;
using Moq;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor.given
{
    public class a_repository_for_a_stateless_aggregate_root : a_command_context
    {
        protected static AggregateRootRepositoryFor<SimpleStatelessAggregateRoot> repository;
        protected static Mock<IApplicationArtifactIdentifier> application_artifact_identifier;

        Establish context = ()=>
        {
            command_context_mock = new Mock<ICommandContext>();
            repository = new AggregateRootRepositoryFor<SimpleStatelessAggregateRoot>(
                command_context_manager.Object,
                event_store.Object,
                event_source_versions.Object,
                application_artifacts.Object,
                logger.Object);
            command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);

            application_artifact_identifier = new Mock<IApplicationArtifactIdentifier>();
            application_artifacts.Setup(a => a.GetIdentifierFor(typeof(SimpleStatelessAggregateRoot))).Returns(application_artifact_identifier.Object);
        };
    }
}