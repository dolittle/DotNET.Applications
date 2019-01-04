/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Dynamic;
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_no_command_handlers : given.a_command_handler_invoker_with_no_command_handlers
    {
        protected static bool result;

        Because of = () => result = invoker.TryHandle(command_request);

        It should_return_false_when_trying_to_handle = () => result.ShouldBeFalse();
    }
}
