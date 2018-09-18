using Dolittle.DependencyInversion;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Moq;
using Machine.Specifications;
using System.Reflection;
using System.Linq;

namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.given
{
    public class TestEventProcessor 
    {
        public int JustTheEventCalled { get; private set; }
        public int TheEventAndTheIdCalled { get; private set; }
        public int TheEventAndTheMetadataCalled { get; private set; }

        public void JustTheEvent(MyEvent @event)
        {

        }

        public void TheEventAndTheId(MyEvent @event, EventSourceId id)
        {

        }

        public void TheEventAndTheMetadata(MyEvent @event, EventMetadata metadata)
        {

        }

        public void AnInvalidSignature()
        {

        }

        public MyEvent AnInvalidSignatureBecauseOfReturnType(MyEvent @event)
        {
            return @event;
        }
    }
}