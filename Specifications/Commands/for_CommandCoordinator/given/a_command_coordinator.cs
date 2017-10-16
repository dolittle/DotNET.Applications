using doLittle.Commands;
using doLittle.Exceptions;
using doLittle.Security;
using doLittle.Logging;
using Machine.Specifications;
using Moq;
using doLittle.Lifecycle;
using doLittle.Applications;
using System.Dynamic;
using doLittle.Globalization;

namespace doLittle.Specs.Commands.for_CommandCoordinator.given
{
    public class a_command_coordinator
    {
        protected static CommandCoordinator coordinator;
        protected static Mock<ICommandHandlerManager> command_handler_manager_mock;
        protected static Mock<ICommandContextManager> command_context_manager_mock;
        protected static Mock<ICommandSecurityManager> command_security_manager_mock;
        protected static Mock<ICommandValidators> command_validators_mock;
        protected static Mock<ICommandContext> command_context_mock;
        protected static Mock<IExceptionPublisher> exception_publisher_mock;
        protected static Mock<ILocalizer> localizer_mock;
        protected static Mock<ILogger> logger;
        protected static CommandRequest command;

        Establish context = () =>
                                {
                                    command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
                                    command_handler_manager_mock = new Mock<ICommandHandlerManager>();
                                    command_context_manager_mock = new Mock<ICommandContextManager>();
                                    command_validators_mock = new Mock<ICommandValidators>();
                                    exception_publisher_mock = new Mock<IExceptionPublisher>();

                                    command_context_mock = new Mock<ICommandContext>();
                                    command_context_manager_mock.Setup(c => c.EstablishForCommand(Moq.It.IsAny<CommandRequest>())).
                                        Returns(command_context_mock.Object);
                                    command_security_manager_mock = new Mock<ICommandSecurityManager>();
                                    command_security_manager_mock.Setup(
                                        s => s.Authorize(Moq.It.IsAny<CommandRequest>()))
                                                                 .Returns(new AuthorizationResult());

                                    logger = new Mock<ILogger>();
                                    localizer_mock = new Mock<ILocalizer>();
                                    localizer_mock.Setup(l => l.BeginScope()).Returns(LocalizationScope.FromCurrentThread);

                                    coordinator = new CommandCoordinator(
                                        command_handler_manager_mock.Object,
                                        command_context_manager_mock.Object,
                                        command_security_manager_mock.Object,
                                        command_validators_mock.Object,
                                        localizer_mock.Object,
                                        exception_publisher_mock.Object,
                                        logger.Object
                                        );
                                };
    }
}
