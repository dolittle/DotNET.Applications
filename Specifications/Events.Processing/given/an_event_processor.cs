using Dolittle.DependencyInversion;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Moq;
using Machine.Specifications;
using System.Reflection;
using System.Linq;
using Dolittle.Logging;
using System;
using Dolittle.Artifacts;
using Dolittle.Types;
using System.Collections.Generic;
using Dolittle.Runtime.Events.Processing;

namespace Dolittle.Events.Processing.given
{
    public class event_processors
    {
        public static Mock<IObjectFactory> object_factory;
        public static Mock<IContainer> container;
        public static Mock<IArtifactTypeMap> artifact_type_map;
        public static Mock<ILogger> logger;
        public static TestEventProcessor processor;
        public static MethodInfo method_with_just_event;
        public static MethodInfo method_with_event_and_event_source_id;
        public static MethodInfo method_with_event_and_metadata;
        public static MethodInfo invalid_method;
        public static MethodInfo invalid_method_return_value;
        public static MethodInfo process_1;    
        public static MethodInfo process_2;
        public static MethodInfo process_3;
        public static Artifact my_event_artifact;
        public static Mock<IImplementationsOf<ICanProcessEvents>> implementations;

        public static IEnumerable<EventProcessorId> event_processor_ids;

        Establish context = () => 
        {
            my_event_artifact = new Artifact(Guid.NewGuid(),1);
            logger = new Mock<ILogger>();
            logger.Setup(l => l.Trace(Moq.It.IsAny<string>(),Moq.It.IsAny<string>(),Moq.It.IsAny<int>(),Moq.It.IsAny<string>()))
                .Callback<string,string,int,string>((s1,s2,i,s3) => Console.WriteLine(s1));
            object_factory = build_object_factory();
            processor = new TestEventProcessor();
            container = build_container(processor);
            implementations = build_implementations();
            artifact_type_map = build_artifact_type_map();
            setup_method_infos();
            identify_event_processors(method_with_just_event,method_with_event_and_event_source_id,method_with_event_and_metadata,process_1,process_2,process_3);
        };

        static Mock<IObjectFactory> build_object_factory() 
        {
            var factory = new Mock<IObjectFactory>();
            factory.Setup(f => f.Build(typeof(MyEvent),Moq.It.IsAny<PropertyBag>())).Returns(new MyEvent());
            return factory;

        }
        static Mock<IContainer> build_container(TestEventProcessor processor)
        {
            var container = new Mock<IContainer>();
            container.Setup(c => c.Get(typeof(TestEventProcessor))).Returns(processor);
            return container;
        }

        static Mock<IImplementationsOf<ICanProcessEvents>> build_implementations()
        {
            var types = new List<Type>();
            types.Add(typeof(TestEventProcessor));
            types.Add(typeof(AnotherTestEventProcessor));
            var implementations = new Mock<IImplementationsOf<ICanProcessEvents>>();
            implementations.Setup(_ => _.GetEnumerator()).Returns(() => types.GetEnumerator());
            return implementations;
        }
 
        static Mock<IArtifactTypeMap> build_artifact_type_map()
        {
            var map = new Mock<IArtifactTypeMap>();
            map.Setup(_ => _.GetArtifactFor(typeof(MyEvent))).Returns(my_event_artifact);
            return map;
        }

        static void setup_method_infos()
        {
            var methods = typeof(TestEventProcessor).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            method_with_just_event = methods.Single(m => m.Name == nameof(TestEventProcessor.JustTheEvent));
            method_with_event_and_event_source_id = methods.Single(m => m.Name == nameof(TestEventProcessor.TheEventAndTheId));
            method_with_event_and_metadata = methods.Single(m => m.Name == nameof(TestEventProcessor.TheEventAndTheMetadata));
            invalid_method = methods.Single(m => m.Name == nameof(TestEventProcessor.AnInvalidSignature));
            invalid_method_return_value = methods.Single(m => m.Name == nameof(TestEventProcessor.AnInvalidSignatureBecauseOfReturnType));

            var another_methods = typeof(AnotherTestEventProcessor).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            process_1 = another_methods.Single(m => m.Name == nameof(AnotherTestEventProcessor.Process1));
            process_2 = another_methods.Single(m => m.Name == nameof(AnotherTestEventProcessor.Process2));
            process_3 = another_methods.Single(m => m.Name == nameof(AnotherTestEventProcessor.Process3));
        }

        static void identify_event_processors(params MethodInfo[] methods)
        {
            event_processor_ids = new List<EventProcessorId>(methods.Select(m => m.EventProcessorId()));
        }
    }
}