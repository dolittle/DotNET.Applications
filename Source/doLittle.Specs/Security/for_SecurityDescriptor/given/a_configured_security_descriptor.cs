using doLittle.Configuration;
using doLittle.Configuration.Defaults;
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Security;
using doLittle.SomeRandomNamespace;
using doLittle.Specs.Security.Fakes;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Security.for_SecurityDescriptor.given
{
    public class a_configured_security_descriptor
    {
        protected static SecurityDescriptor security_descriptor;
        protected static SimpleCommand command_that_has_namespace_and_type_rule;
        protected static AnotherSimpleCommand command_that_has_namespace_rule;
        protected static CommandInADifferentNamespace command_that_is_not_applicable;
        protected static Mock<ICanResolvePrincipal> resolve_principal_mock;

        Establish context = () =>
            {
                resolve_principal_mock = new Mock<ICanResolvePrincipal>();
                var currentConfigure = Configure.With(Mock.Of<IContainer>(), (IDefaultConventions) null, null, null);
                Mock.Get(currentConfigure.Container)
                    .Setup(m => m.Get<ICanResolvePrincipal>())
                    .Returns(resolve_principal_mock.Object);

                security_descriptor = new SecurityDescriptor();
                command_that_has_namespace_and_type_rule = new SimpleCommand();
                command_that_has_namespace_rule = new AnotherSimpleCommand();
                command_that_is_not_applicable = new CommandInADifferentNamespace();
            };
    }
}