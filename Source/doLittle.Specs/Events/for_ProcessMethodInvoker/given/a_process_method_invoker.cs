using doLittle.Events;
using Machine.Specifications;

namespace doLittle.Specs.Events.for_ProcessMethodInvoker.given
{
    public class a_process_method_invoker
    {
        protected static ProcessMethodInvoker invoker;

        Establish context = () => invoker = new ProcessMethodInvoker();
    }
}
