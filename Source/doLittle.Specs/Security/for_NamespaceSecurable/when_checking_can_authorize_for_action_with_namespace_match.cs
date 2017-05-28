using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Security.for_NamespaceSecurable
{
    [Subject(typeof(NamespaceSecurable))]
    public class when_checking_can_authorize_for_action_with_namespace_match : given.a_namespace_securable
    {
        static bool authorized;

        Because of = () => authorized = namespace_securable.CanAuthorize(action_with_namespace_match);

        It should_be_authorized = () => authorized.ShouldBeTrue();
    }
}