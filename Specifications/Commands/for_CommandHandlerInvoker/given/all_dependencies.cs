/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications;
using Dolittle.Commands;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Commands.for_CommandHandlerInvoker.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;
        protected static Mock<IApplicationResources> application_resources;
        protected static Mock<ICommandRequestConverter> command_request_converter;

        protected static Mock<ILogger> logger;

        Establish context = () =>
        {
            type_finder = new Mock<ITypeFinder>();
            type_finder.Setup(t => t.FindMultiple<ICanHandleCommands>()).Returns(new Type[0]);
            container = new Mock<IContainer>();
            application_resources = new Mock<IApplicationResources>();
            command_request_converter = new Mock<ICommandRequestConverter>();
            logger = new Mock<ILogger>();
        };
    }
}
