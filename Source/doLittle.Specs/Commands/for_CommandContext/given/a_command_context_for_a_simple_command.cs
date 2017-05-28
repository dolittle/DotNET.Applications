using doLittle.Applications;
using doLittle.Commands;
using doLittle.Events;
using doLittle.Lifecycle;
using Machine.Specifications;
using Moq;
using System.Dynamic;

namespace doLittle.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command
    {
        protected static CommandRequest command;
        protected static CommandContext command_context;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator;
        protected static Mock<IEventEnvelopes> event_envelopes;

        Establish context = () =>
        {
            command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
            uncommitted_event_stream_coordinator = new Mock<IUncommittedEventStreamCoordinator>();
            event_envelopes = new Mock<IEventEnvelopes>();
            command_context = new CommandContext(command, null, uncommitted_event_stream_coordinator.Object);
        };
    }
}
