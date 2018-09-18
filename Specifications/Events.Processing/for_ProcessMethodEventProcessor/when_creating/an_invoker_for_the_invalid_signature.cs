namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.when_creating
{
    using System;
    using Dolittle.Artifacts;
    using Machine.Specifications;

    [Subject(typeof(ProcessMethodEventProcessor), "Create")]
    public class an_invoker_for_the_invalid_signature : given.an_event_processor
    {
        static Exception exception;
        static ProcessMethodEventProcessor result;


        Because of = () => exception = Catch.Exception(() => result = new ProcessMethodEventProcessor(object_factory.Object,container.Object,Guid.NewGuid(),new Artifact(Guid.NewGuid(),1),typeof(given.MyEvent),invalid_method, logger.Object));

        It should_not_create_the_process_method_event_processor = () => result.ShouldBeNull();
        It should_throw_an_invalid_method_exception = () => exception.ShouldBeOfExactType<EventProcessorMethodParameterMismatch>();
    }    
}