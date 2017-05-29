using System;
using doLittle.Applications;
using doLittle.Commands;
using doLittle.Execution;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Commands.for_CommandHandlerInvoker.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeDiscoverer> type_discoverer;
        protected static Mock<IContainer> container;
        protected static Mock<IApplicationResources> application_resources;
        protected static Mock<ICommandRequestConverter> command_request_converter;

        Establish context = () =>
        {
            type_discoverer = new Mock<ITypeDiscoverer>();
            type_discoverer.Setup(t => t.FindMultiple<IHandleCommands>()).Returns(new Type[0]);
            container = new Mock<IContainer>();
            application_resources = new Mock<IApplicationResources>();
            command_request_converter = new Mock<ICommandRequestConverter>();
        };
    }
}
