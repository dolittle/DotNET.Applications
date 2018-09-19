using Dolittle.DependencyInversion;
using Dolittle.Events;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events;
using Moq;
using Machine.Specifications;
using System.Reflection;
using System.Linq;

namespace Dolittle.Events.Processing.given
{
    public class TestEventProcessor : ICanProcessEvents
    {
        public int JustTheEventCalled { get; private set; }
        public int TheEventAndTheIdCalled { get; private set; }
        public int TheEventAndTheMetadataCalled { get; private set; }

        [EventProcessor("77ca7665-605f-4cda-8dbc-8b9640328e13")]
        public void JustTheEvent(MyEvent @event)
        {
            JustTheEventCalled++;
        }

        [EventProcessor("c3394c93-ef16-498d-8bc5-28254624f1df")]
        public void TheEventAndTheId(MyEvent @event, EventSourceId id)
        {
            TheEventAndTheIdCalled++;
        }

        [EventProcessor("a96c9899-e0ea-4fb0-9172-813d297dca0b")]
        public void TheEventAndTheMetadata(MyEvent @event, EventMetadata metadata)
        {
            TheEventAndTheMetadataCalled++;
        }

        public void AValidSignatureButNotMarkedAsAnEventProcessor(MyEvent @event)
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

    public class AnotherTestEventProcessor : ICanProcessEvents
    {
        [EventProcessor("5f752ea3-eb93-47db-9837-f1aa25f6806c")]
        public void Process1(MyEvent @event)
        {

        }

        [EventProcessor("8038aa08-4c25-4c11-a243-eac7febeb971")]
        public void Process2(MyEvent @event, EventSourceId id)
        {

        }

        [EventProcessor("bfaf129c-f8e4-4dc0-857d-338839be08c9")]
        public void Process3(MyEvent @event, EventMetadata metadata)
        {

        }
    }
}