using Dolittle.Applications;
using Dolittle.Domain;
using Machine.Specifications;
using Moq;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor.given
{
    public class a_repository_for_a_stateful_aggregate_root : a_command_context
	{
		protected static AggregateRootRepositoryFor<SimpleStatefulAggregateRoot> repository;
        protected static Mock<IApplicationArtifactIdentifier> application_artifact_identifier;

        Establish context = () =>
		                    {
								repository = new AggregateRootRepositoryFor<SimpleStatefulAggregateRoot>(
                                    command_context_manager.Object, 
                                    event_store.Object, 
                                    event_source_versions.Object, 
                                    application_artifacts.Object,
                                    logger.Object);
		                        command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);

                                application_artifact_identifier = new Mock<IApplicationArtifactIdentifier>();
                                application_artifacts.Setup(a => a.Map(typeof(SimpleStatefulAggregateRoot))).Returns(application_artifact_identifier.Object);
                            };
	}
}
