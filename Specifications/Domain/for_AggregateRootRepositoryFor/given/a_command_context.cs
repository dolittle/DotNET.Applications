/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Runtime.Commands.Coordination;
using Machine.Specifications;
using Moq;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor.given
{
    public class a_command_context : all_dependencies
    {
        protected static Mock<ICommandContext> command_context_mock;

        Establish context = ()=>
        {
            command_context_mock = new Mock<ICommandContext>();
            command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);
        };
    }
}