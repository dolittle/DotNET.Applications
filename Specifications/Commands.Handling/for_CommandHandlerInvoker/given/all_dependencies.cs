/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Commands;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;
        protected static Mock<IArtifactTypeMap> artifact_type_map;
        protected static Mock<ICommandRequestToCommandConverter> command_request_converter;
        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            type_finder = new Mock<ITypeFinder>();
            type_finder.Setup(t => t.FindMultiple<ICanHandleCommands>()).Returns(new Type[0]);
            container = new Mock<IContainer>();
            artifact_type_map = new Mock<IArtifactTypeMap>();
            command_request_converter = new Mock<ICommandRequestToCommandConverter>();
            logger = new Mock<ILogger>();
        };
    }
}
