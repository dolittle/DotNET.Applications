using Machine.Specifications;

namespace doLittle.Events.Processing.Specs.for_ProcessMethodInvoker.given
{
    public class a_process_method_invoker
    {
        protected static ProcessMethodInvoker invoker;

        Establish context = () => invoker = new ProcessMethodInvoker();
    }
}
