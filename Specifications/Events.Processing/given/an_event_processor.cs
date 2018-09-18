using Dolittle.DependencyInversion;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Moq;
using Machine.Specifications;
using System.Reflection;
using System.Linq;
using Dolittle.Logging;

namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.given
{
    public class an_event_processor
    {
        public static Mock<IObjectFactory> object_factory;
        public static Mock<IContainer> container;
        
        public static Mock<ILogger> logger;
        public static TestEventProcessor processor;
        public static MethodInfo method_with_just_event;
        public static MethodInfo method_with_event_and_event_source_id;
        public static MethodInfo method_with_event_and_metadata;
        public static MethodInfo invalid_method;
        public static MethodInfo invalid_method_return_value;

        Establish context = () => 
        {
            logger = new Mock<ILogger>();
            object_factory = build_object_factory();
            processor = new TestEventProcessor();
            container = build_container(processor);
            setup_method_infos();
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

        static void setup_method_infos()
        {
            var methods = typeof(TestEventProcessor).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            method_with_just_event = methods.Single(m => m.Name == nameof(TestEventProcessor.JustTheEvent));
            method_with_event_and_event_source_id = methods.Single(m => m.Name == nameof(TestEventProcessor.TheEventAndTheId));
            method_with_event_and_metadata = methods.Single(m => m.Name == nameof(TestEventProcessor.TheEventAndTheMetadata));
            invalid_method = methods.Single(m => m.Name == nameof(TestEventProcessor.AnInvalidSignature));
            invalid_method_return_value = methods.Single(m => m.Name == nameof(TestEventProcessor.AnInvalidSignatureBecauseOfReturnType));
        }
    }
}