using doLittle.Security;
using doLittle.SomeRandomNamespace;
using doLittle.Specs.Security.Fakes;

namespace doLittle.Specs.Security.for_TypeSecurable.given
{
    public class a_type_securable
    {
        protected static TypeSecurable type_securable;
        protected static object action_of_secured_type;
        protected static object action_of_another_type;

        public a_type_securable()
        {
            action_of_secured_type = new SimpleCommand();
            action_of_another_type = new CommandInADifferentNamespace();

            type_securable = new TypeSecurable(action_of_secured_type.GetType());
        }
    }
}