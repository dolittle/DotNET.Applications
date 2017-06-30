using System;
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Security.for_SecurityManager.given
{
    public class a_security_manager_with_no_descriptors
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;
        protected static SecurityManager security_manager;

        Establish context = () =>
            {
                type_finder = new Mock<ITypeFinder>();
                container = new Mock<IContainer>();
                type_finder.Setup(d => d.FindMultiple(typeof(ISecurityDescriptor)))
                                    .Returns(new Type[]{});

                security_manager = new SecurityManager(type_finder.Object, container.Object);
            };
    }
}