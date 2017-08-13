using doLittle.Types;
using Machine.Specifications;
using Moq;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace doLittle.Ninject.Specs.for_KernelExtensions.given
{
    public class a_kernel_and_a_type_importer
    {
        protected static Mock<IInstancesOf<NinjectModule>> ninject_modules;
        protected static Mock<IKernel> kernel_mock;

        Establish context = () =>
        {
            ninject_modules = new Mock<IInstancesOf<NinjectModule>>();
            kernel_mock = new Mock<IKernel>();
            kernel_mock.Setup(k => k.Resolve(Moq.It.IsAny<IRequest>())).Returns(new[] { ninject_modules.Object });
        };

    }
}
