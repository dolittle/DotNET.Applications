/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Runtime.Commands.Handling.for_CommandHandlerInvoker.given
{
    public class a_command_handler_invoker_with_no_command_handlers : all_dependencies
    {
        protected static CommandHandlerInvoker invoker;

        Establish context = () => invoker = new CommandHandlerInvoker(type_finder.Object, container.Object, application_resources.Object, command_request_converter.Object, logger.Object); 
    }
}
