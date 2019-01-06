/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Dolittle.Artifacts;
using Dolittle.Runtime.Commands;
using Dolittle.Execution;
using System.Collections.Generic;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker.given
{
    public class a_command_handler_invoker_with_no_command_handlers : all_dependencies
    {
        protected static CommandHandlerInvoker invoker;
        protected static Artifact command_artifact;
        protected static CommandRequest command_request;

        Establish context = () => 
        {
            command_artifact = Artifact.New();
            command_request = new CommandRequest(CorrelationId.New(),command_artifact.Id, command_artifact.Generation, new Dictionary<string,object>());
            invoker = new CommandHandlerInvoker(type_finder.Object, container.Object, artifact_type_map.Object, command_request_converter.Object, logger.Object);
        };
    }
}
