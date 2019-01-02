using Machine.Specifications;
using Moq;
using Dolittle.Commands.Handling;
using Dolittle.Security;
using Dolittle.Runtime.Commands;

namespace Dolittle.Commands.Security.for_CommandRequestSecurityManager.given
{
    public class a_command_request_security_manager
    {
        protected static Mock<ICommandRequestToCommandConverter> converter_mock;
        protected static Mock<ISecurityManager> security_manager_mock;
        protected static CommandRequestSecurityManager command_security_manager;
        protected static ICommand command;

        Establish context = () =>
        {
            security_manager_mock = new Mock<ISecurityManager>();
            converter_mock = new Mock<ICommandRequestToCommandConverter>();
            command = Mock.Of<ICommand>();
            converter_mock.Setup(m => m.Convert(Moq.It.IsAny<CommandRequest>())).Returns(command);
            command_security_manager = new CommandRequestSecurityManager(security_manager_mock.Object, converter_mock.Object);
        };
    }
}