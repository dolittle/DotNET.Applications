namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.when_creating
{
    using System;
    using Dolittle.Artifacts;
    using Machine.Specifications;

    [Subject(typeof(ProcessMethodEventProcessor), "Create")]
    public class an_invoker_for_the_event_and_event_source_id_signature : given.an_event_processor
    {
        static ProcessMethodEventProcessor result;

        Because of = () => result = new ProcessMethodEventProcessor(object_factory.Object,container.Object,Guid.NewGuid(),new Artifact(Guid.NewGuid(),1),typeof(given.MyEvent),method_with_event_and_event_source_id, logger.Object);

        It should_create_the_process_method_event_processor = () => result.ShouldNotBeNull();
    }  
}