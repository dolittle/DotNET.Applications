using System.Dynamic;
using System.Threading;
using Bifrost.Applications;
using Bifrost.Commands;
using Bifrost.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
    [Subject(Subjects.establishing_context)]
    public class when_establishing_on_different_threads_with_same_command : given.a_command_context_manager
    {
        static ICommandContext firstCommandContext;
        static ICommandContext secondCommandContext;

        Establish context = () =>
                                {
                                    var resetEvent = new ManualResetEvent(false);
                                    var command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
                                    firstCommandContext = Manager.EstablishForCommand(command);
                                    var thread = new Thread(
                                        () =>
                                            {
                                                secondCommandContext = Manager.EstablishForCommand(command);
                                                resetEvent.Reset();
                                            }
                                        );
                                    thread.Start();
                                    resetEvent.WaitOne(1000);
                                };

        It should_return_different_contexts = () => firstCommandContext.ShouldNotEqual(secondCommandContext);
    }
}