// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker.given
{
    public class a_command_handler_invoker_with_one_command_handler : a_command_handler_invoker_with_no_command_handlers
    {
        protected static CommandHandler handler;

        Establish context = () =>
        {
            artifact_type_map.Setup(a => a.GetArtifactFor(Moq.It.IsAny<System.Type>())).Returns(command_artifact);
            handler = new CommandHandler();
            type_finder.Setup(t => t.FindMultiple<ICanHandleCommands>()).Returns(new[] { typeof(CommandHandler) });

            container.Setup(c => c.Get(typeof(CommandHandler))).Returns(handler);
        };
    }
}