using System;
using Machine.Specifications;

namespace doLittle.Events.Processing.Specs.for_ProcessMethodInvoker
{
    public class when_invoking_on_an_instance_that_can_handle_a_given_event : given.a_process_method_invoker
    {
        static TypeWithSimpleEventProcessMethod instance;
        static SimpleEvent @event;
        static bool result;

        Establish context = () =>
                                {
                                    instance = new TypeWithSimpleEventProcessMethod();
                                    @event = new SimpleEvent();
                                };


        Because of = () => result = invoker.TryProcess(instance, @event);

        It should_result_in_true = () => result.ShouldBeTrue();
        It should_have_called_handle_method = () => instance.ProcessCalled.ShouldBeTrue();
        It should_pass_in_the_command = () => instance.EventPassed.ShouldEqual(@event);
    }
}
