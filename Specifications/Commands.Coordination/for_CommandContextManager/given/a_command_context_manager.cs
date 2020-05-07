// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Execution;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Commands.Coordination.for_CommandContextManager.given
{
    public class a_command_context_manager
    {
        protected static CommandContextManager manager;
        protected static Mock<IEventStore> event_store;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        protected static Mock<IEventProcessingCompletion> event_processing_completion;
        protected static CommandContextFactory factory;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            CommandContextManager.ResetContext();
            event_store = new Mock<IEventStore>();
            execution_context_manager = new Mock<IExecutionContextManager>();
            event_processing_completion = new Mock<IEventProcessingCompletion>();
            logger = new Mock<ILogger>();

            factory = new CommandContextFactory(
                event_store.Object,
                execution_context_manager.Object,
                event_processing_completion.Object,
                logger.Object);

            manager = new CommandContextManager(factory);
        };
    }
}
