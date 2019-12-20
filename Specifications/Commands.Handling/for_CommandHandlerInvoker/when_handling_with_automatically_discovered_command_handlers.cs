// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_automatically_discovered_command_handlers : given.a_command_handler_invoker_with_one_command_handler
    {
        static bool result;

        Establish context = () => command_request_converter.Setup(c => c.Convert(command_request)).Returns(new Command());

        Because of = () => result = invoker.TryHandle(command_request);

        It should_return_true_when_trying_to_handle = () => result.ShouldBeTrue();
    }
}