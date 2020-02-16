// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Security;
using Machine.Specifications;
using Moq;

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