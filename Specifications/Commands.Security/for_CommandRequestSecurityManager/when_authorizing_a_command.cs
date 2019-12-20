// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Dynamic;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Commands.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Security.for_CommandRequestSecurityManager
{
    public class when_authorizing_a_command : given.a_command_request_security_manager
    {
        static CommandRequest command_request;

        Establish context = () =>
        {
            var artifact = Artifact.New();
            command_request = new CommandRequest(CorrelationId.Empty, artifact.Id, artifact.Generation, new ExpandoObject());
        };

        Because of = () => command_security_manager.Authorize(command_request);

        It should_convert_the_request_to_the_command = () => converter_mock.Verify(c => c.Convert(command_request), Moq.Times.Once());
        It should_delegate_the_request_for_security_to_the_security_manager = () => security_manager_mock.Verify(s => s.Authorize<HandleCommand>(command), Moq.Times.Once());
    }
}