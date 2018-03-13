using System.Security.Principal;
using Dolittle.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(SecurityDescriptor))]
    public class when_authorizing_on_command_type_and_namespace_and_user_is_in_roles : given.a_configured_security_descriptor
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;

        Establish context = () =>
        {
            resolve_principal_mock.Setup(m => m.Resolve()).Returns(
                new GenericPrincipal(
                    new GenericIdentity(""),
                    new[]
                    {
                        Fakes.SecurityDescriptor.NAMESPACE_ROLE,
                        Fakes.SecurityDescriptor.SIMPLE_COMMAND_ROLE
                    }));
        };

        Because of = () => authorize_descriptor_result = security_descriptor.Authorize(command_that_has_namespace_and_type_rule);

        It should_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeTrue();
    }
}