using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Security.for_SecurityDescriptor
{
    public class when_configuring
    {
        static BaseSecurityDescriptor   descriptor;

        Because of = () => descriptor = new BaseSecurityDescriptor();

        It should_have_a_when_clause_set = () => descriptor.When.ShouldNotBeNull();
    }
}
