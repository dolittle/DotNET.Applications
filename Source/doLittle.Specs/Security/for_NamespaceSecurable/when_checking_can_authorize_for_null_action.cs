using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Security.for_NamespaceSecurable
{
    [Subject(typeof(NamespaceSecurable))]
    public class when_checking_can_authorize_for_null_action : given.a_namespace_securable
    {
        static bool authorized;

        Because of = () => authorized = namespace_securable.CanAuthorize(null);

        It should_not_be_authorized = () => authorized.ShouldBeFalse();
    }
}