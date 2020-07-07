// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Security;
using Dolittle.Tenancy;
using Machine.Specifications;
using Moq;

namespace Dolittle.Commands.Coordination.for_CommandContextManager.given
{
    public class a_command_context_manager
    {
        protected static CommandContextManager manager;
        protected static Mock<IEventStore> event_store;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        protected static ExecutionContext execution_context;
        protected static Mock<IEventProcessingCompletion> event_processing_completion;
        protected static TenantId tenant;
        protected static CommandContextFactory factory;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            CommandContextManager.ResetContext();
            tenant = Guid.Parse("60574bab-60a8-4c75-9940-e5f53c79df73");
            event_store = new Mock<IEventStore>();
            execution_context_manager = new Mock<IExecutionContextManager>();
            execution_context = new ExecutionContext(
                    Guid.Parse("2e1224b5-2534-4a4d-bb22-12013a690288"),
                    tenant,
                    new Versioning.Version(1, 2, 3, 4, ""),
                    Execution.Environment.Development,
                    Guid.Parse("7280bdc8-72d0-48bd-8ea3-6a1f6ffa1bd2"),
                    Claims.Empty,
                    CultureInfo.InvariantCulture);
            execution_context_manager
                .SetupGet(_ => _.Current).Returns(() => execution_context);
            event_processing_completion = new Mock<IEventProcessingCompletion>();
            logger = new Mock<ILogger>();

            factory = new CommandContextFactory(
                event_store.Object,
                execution_context_manager.Object,
                event_processing_completion.Object,
                logger.Object);

            manager = new CommandContextManager(factory, execution_context_manager.Object, Mock.Of<ILogger>());
        };
    }
}
