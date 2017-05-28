using doLittle.Commands;
using doLittle.Domain;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Domain.for_AggregateRootRepository.given
{
    public class a_command_context : all_dependencies
	{
		protected static AggregateRootRepository<SimpleStatefulAggregateRoot> repository;
	    protected static Mock<ICommandContext> command_context_mock;

		Establish context = () =>
		                    {
                                command_context_mock = new Mock<ICommandContext>();
		                        command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);
		                    };
	}
}
