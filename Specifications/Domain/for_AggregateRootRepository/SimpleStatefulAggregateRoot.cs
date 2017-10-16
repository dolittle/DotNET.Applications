using doLittle.Domain;
using doLittle.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class SimpleStatefulAggregateRoot : AggregateRoot
    {
        public int SimpleEventHandled;

        public SimpleStatefulAggregateRoot(EventSourceId id) : base(id)
        {
        }

        void On(SimpleEvent @event)
        {
            SimpleEventHandled++;
        }

        public bool ReApplyCalled = false;
        public CommittedEventStream EventStreamApplied;

        public override void ReApply(CommittedEventStream eventStream)
        {
            ReApplyCalled = true;
            EventStreamApplied = eventStream;
            base.ReApply(eventStream);
        }
    }
}
