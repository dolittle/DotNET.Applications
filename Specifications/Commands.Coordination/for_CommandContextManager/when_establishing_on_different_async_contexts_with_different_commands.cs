// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        static CommandRequest firstCommand;
        static CommandRequest secondCommand;
        static ICommandContext firstCommandContext;
        static ICommandContext secondCommandContext;

        Establish context = () =>
        {
            var firstArtifact = Artifact.New();
            firstCommand = new CommandRequest(CorrelationId.Empty, firstArtifact.Id, firstArtifact.Generation, new ExpandoObject());
            var secondArtifact = Artifact.New();
            secondCommand = new CommandRequest(CorrelationId.Empty, secondArtifact.Id, secondArtifact.Generation, new ExpandoObject());
            Task.Run(() => firstCommandContext = manager.EstablishForCommand(firstCommand)).GetAwaiter().GetResult();
            Task.Run(() => secondCommandContext = manager.EstablishForCommand(secondCommand)).GetAwaiter().GetResult();
        };

        It should_return_different_contexts = () => firstCommandContext.ShouldNotEqual(secondCommandContext);
    }
}