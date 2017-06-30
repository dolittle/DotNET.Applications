using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Security.for_SecurityManager.given
{
    public class a_security_manager_with_discovered_descriptors
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;
        protected static SecurityManager security_manager;

        protected static Mock<ISecurityDescriptor> first_security_descriptor;
        protected static Mock<ISecurityDescriptor> second_security_descriptor;

        Establish context = () =>
        {
            first_security_descriptor = new Mock<ISecurityDescriptor>();
            second_security_descriptor = new Mock<ISecurityDescriptor>();

            type_finder = new Mock<ITypeFinder>();
            container = new Mock<IContainer>();
            type_finder.Setup(d => d.FindMultiple<ISecurityDescriptor>())
                .Returns(new[] { typeof(ISecurityDescriptor), typeof(BaseSecurityDescriptor) });

            container.Setup(r => r.Get(typeof(ISecurityDescriptor))).Returns(first_security_descriptor);
            container.Setup(r => r.Get(typeof(BaseSecurityDescriptor))).Returns(second_security_descriptor);

            security_manager = new SecurityManager(type_finder.Object, container.Object);
        };
    }
}
