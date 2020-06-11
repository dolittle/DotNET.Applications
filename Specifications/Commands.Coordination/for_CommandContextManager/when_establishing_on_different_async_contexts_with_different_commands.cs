// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Dynamic;
using System.Threading.Tasks;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Coordination.for_CommandContextManager
{
    [Subject(Subjects.establishing_context)]
    public class when_establishing_on_different_async_contexts_with_different_commands : given.a_command_context_manager
    {
        static CorrelationId first_correlation;
        static CorrelationId second_correlation;
        static CommandRequest first_command;
        static CommandRequest second_command;
        static ICommandContext first_command_context;
        static ICommandContext second_command_context;

        Establish context = () =>
        {
            var firstArtifact = new Artifact(Guid.Parse("0b5e6d53-473c-4715-b4e4-5517d0a2eb25"), 3);
            var secondArtifact = new Artifact(Guid.Parse("a07622ee-f719-4052-b7e9-10d9af555285"), 5);
            first_correlation = Guid.Parse("65a6e42f-8e18-4c36-9417-acd1717aa468");
            second_correlation = Guid.Parse("65a6e42f-8e18-4c36-9417-acd1717aa468");
            first_command = new CommandRequest(first_correlation, firstArtifact.Id, firstArtifact.Generation, new ExpandoObject());
            second_command = new CommandRequest(second_correlation, secondArtifact.Id, secondArtifact.Generation, new ExpandoObject());
            Task.Run(() => first_command_context = manager.EstablishForCommand(first_command)).GetAwaiter().GetResult();
            Task.Run(() => second_command_context = manager.EstablishForCommand(second_command)).GetAwaiter().GetResult();
        };

        It should_return_different_contexts = () => first_command_context.ShouldNotEqual(second_command_context);
    }
}