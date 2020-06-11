// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Dynamic;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Coordination.for_CommandContextManager
{
    [Subject(Subjects.establishing_context)]
    public class when_establishing_for_same_command : given.a_command_context_manager
    {
        static Artifact artifact;
        static CorrelationId correlation;
        static CommandRequest command;
        static ICommandContext commandContext;

        Establish context = () =>
        {
            artifact = new Artifact(Guid.Parse("682dc523-8598-4a5a-be8a-dde1770fa615"), 2);
            correlation = Guid.Parse("a17cfc29-16f2-4527-a682-ce0cf6e8b40b");
            command = new CommandRequest(correlation, artifact.Id, artifact.Generation, new ExpandoObject());
            execution_context = new ExecutionContext(
                execution_context_manager.Object.Current.Microservice,
                tenant,
                execution_context_manager.Object.Current.Version,
                execution_context_manager.Object.Current.Environment,
                correlation,
                execution_context_manager.Object.Current.Claims,
                execution_context_manager.Object.Current.Culture);
        };

        Because of = () => commandContext = manager.EstablishForCommand(command);

        It should_return_context = () => commandContext.ShouldNotBeNull();
        It should_return_context_with_command_in_it = () => commandContext.Command.ShouldEqual(command);
        It should_return_context_with_correct_execution_context = () => commandContext.ExecutionContext.ShouldEqual(execution_context);
        It should_set_the_correct_execution_context = () => execution_context_manager.Verify(_ => _.CurrentFor(tenant, correlation, Moq.It.IsAny<string>(), Moq.It.IsAny<int>(), Moq.It.IsAny<string>()), Moq.Times.Once);

        It should_return_the_same_calling_it_twice_on_same_thread = () =>
        {
            var secondContext = manager.EstablishForCommand(command);
            secondContext.ShouldEqual(commandContext);
        };
    }
}
