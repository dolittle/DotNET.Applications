using doLittle.Commands;
using doLittle.Execution;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Commands.for_CommandValidators.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICommandValidator>> validators_mock;

        Establish context = () => validators_mock = new Mock<IInstancesOf<ICommandValidator>>();
    }
}
