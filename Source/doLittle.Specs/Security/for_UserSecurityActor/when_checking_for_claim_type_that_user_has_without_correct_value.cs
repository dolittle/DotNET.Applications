using System.Security.Claims;
using doLittle.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_claim_type_that_user_has_without_correct_value : given.a_user_security_actor
    {
        const string claim_type = "Something";
        static bool result;

        Establish context = () => identity.AddClaim(new Claim(claim_type, "42"));

        Because of = () => result = actor.HasClaimTypeWithValue("Something", "43");

        It should_not_have_the_claim = () => result.ShouldBeFalse();
    }
}
