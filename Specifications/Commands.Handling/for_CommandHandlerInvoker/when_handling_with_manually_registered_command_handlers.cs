/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Dynamic;
using Dolittle.Applications;
using Dolittle.Commands;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_manually_registered_command_handlers : given.a_command_handler_invoker_with_no_command_handlers
    {
        static bool result;

        Establish context = () =>
        {
            artifact_type_map.Setup(a => a.GetArtifactFor(typeof(Command))).Returns(command_artifact);
            command_request_converter.Setup(c => c.Convert(command_request)).Returns(new Command());
            container.Setup(c => c.Get(typeof(CommandHandler))).Returns(new CommandHandler());
            invoker.Register(typeof(CommandHandler));
        };

        Because of = () => result = invoker.TryHandle(command_request);

        It should_return_true_when_trying_to_handle = () => result.ShouldBeTrue();
    }
}
