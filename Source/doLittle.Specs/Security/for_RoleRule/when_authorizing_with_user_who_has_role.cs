using System.Threading;
using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Security.for_RoleRule
{
    [Subject(typeof (RoleRule))]
    public class when_authorizing_with_user_who_has_role : given.a_rule_role
    {
        static bool is_authorized;

        Establish context = () => SetUserRole(required_role);

        Because of = () => is_authorized = rule.IsAuthorized(new object());

        It should_be_authorized = () => is_authorized.ShouldBeTrue();
    }
}