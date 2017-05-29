using System;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Security.for_SecurityManager.given
{
    public class a_security_manager_with_no_descriptors
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container;
        protected static SecurityManager security_manager;

        Establish context = () =>
            {
                type_discoverer_mock = new Mock<ITypeDiscoverer>();
                container = new Mock<IContainer>();
                type_discoverer_mock.Setup(d => d.FindMultiple(typeof(ISecurityDescriptor)))
                                    .Returns(new Type[]{});

                security_manager = new SecurityManager(type_discoverer_mock.Object, container.Object);
            };
    }
}