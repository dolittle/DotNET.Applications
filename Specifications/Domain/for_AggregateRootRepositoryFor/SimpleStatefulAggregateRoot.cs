using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
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
