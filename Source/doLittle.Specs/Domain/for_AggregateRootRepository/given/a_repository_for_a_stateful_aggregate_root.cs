using doLittle.Applications;
using doLittle.Commands;
using doLittle.Domain;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Domain.for_AggregateRootRepository.given
{
    public class a_repository_for_a_stateful_aggregate_root : all_dependencies
	{
		protected static AggregateRootRepository<SimpleStatefulAggregateRoot> repository;
	    protected static Mock<ICommandContext> command_context_mock;
        protected static Mock<IApplicationResourceIdentifier> application_resource_identifier;

        Establish context = () =>
		                    {
                                command_context_mock = new Mock<ICommandContext>();
								repository = new AggregateRootRepository<SimpleStatefulAggregateRoot>(
                                    command_context_manager.Object, 
                                    event_store.Object, 
                                    event_source_versions.Object, 
                                    application_resources.Object);
		                        command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);

                                application_resource_identifier = new Mock<IApplicationResourceIdentifier>();
                                application_resources.Setup(a => a.Identify(typeof(SimpleStatefulAggregateRoot))).Returns(application_resource_identifier.Object);
                            };
	}
}
