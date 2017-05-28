using doLittle.Commands;
using doLittle.Security;

namespace doLittle.Specs.Security.Fakes
{
    public class SecurityDescriptor : doLittle.Security.BaseSecurityDescriptor
    {
        public static string SECURED_NAMESPACE = typeof(SimpleCommand).Namespace;
        public const string NAMESPACE_ROLE = "CanExecuteCommandsInNamespace";
        public const string SIMPLE_COMMAND_ROLE = "CanExecuteSimpleCommands";

        public SecurityDescriptor()
        {
            When.Handling().Commands()
                .InNamespace(SECURED_NAMESPACE, n => n.User().MustBeInRole(NAMESPACE_ROLE));
            When.Handling().Commands()
                .InstanceOf<SimpleCommand>(c => c.User().MustBeInRole(SIMPLE_COMMAND_ROLE));
        }
    }
}