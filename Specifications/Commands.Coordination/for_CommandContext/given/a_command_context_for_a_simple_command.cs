// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Dynamic;
using Dolittle.Artifacts;
using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Execution;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Commands.Coordination.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command
    {
        protected static CommandRequest command;
        protected static CommandContext command_context;
        protected static Mock<IEventStore> event_store;
        protected static Mock<IEventProcessingCompletion> event_processing_completion;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            var artifact = Artifact.New();
            command = new CommandRequest(CorrelationId.Empty, artifact.Id, artifact.Generation, new ExpandoObject());
            event_store = new Mock<IEventStore>();
            logger = new Mock<ILogger>();
            event_processing_completion = new Mock<IEventProcessingCompletion>();
            command_context = new CommandContext(command, null, event_store.Object, event_processing_completion.Object, logger.Object);

            event_processing_completion.Setup(e => e.Perform(Moq.It.IsAny<CorrelationId>(), Moq.It.IsAny<IEnumerable<IEvent>>(), Moq.It.IsAny<Action>()))
                .Callback((CorrelationId correlationId, IEnumerable<IEvent> events, Action action) => action());
        };
    }
}
