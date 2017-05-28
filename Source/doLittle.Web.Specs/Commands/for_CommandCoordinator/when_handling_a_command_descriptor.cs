using System;
using System.Dynamic;
using doLittle.Applications;
using doLittle.Commands;
using doLittle.Lifecycle;
using doLittle.Web.Commands;
using Machine.Specifications;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Web.Specs.Commands.for_CommandCoordinator
{
    public class when_handling_a_command_descriptor
    {
        const string connection_id = "My Connection";
        static Mock<ICommandCoordinator> core_command_coordinator;
        static Mock<ICommandContextConnectionManager> command_context_connection_manager;
        static Web.Commands.CommandCoordinator  command_coordinator;
        static CommandRequest command;
        static HubCallerContext hub_caller_context;
        static TransactionCorrelationId correlation_id;
        

        Establish context = () =>
        {
            core_command_coordinator = new Mock<ICommandCoordinator>();
            command_context_connection_manager = new Mock<ICommandContextConnectionManager>();
            command_coordinator = new Web.Commands.CommandCoordinator(
                core_command_coordinator.Object,
                command_context_connection_manager.Object);

            hub_caller_context = new HubCallerContext(Mock.Of<IRequest>(), connection_id);
            command_coordinator.Context = hub_caller_context;

            correlation_id = Guid.NewGuid();
            command = new CommandRequest(correlation_id, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
        };

        Because of = () => command_coordinator.Handle(command);

        It should_register_correlation_id_with_connection_manager = () => command_context_connection_manager.Verify(c => c.Register(connection_id, correlation_id), Times.Once());
        It should_forward_request_to_core_command_coordinator = () => core_command_coordinator.Verify(c => c.Handle(command), Times.Once());
    }
}
