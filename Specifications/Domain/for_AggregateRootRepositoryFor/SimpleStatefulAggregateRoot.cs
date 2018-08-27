using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
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
        public CommittedEvents EventsApplied;

        public override void ReApply(CommittedEvents eventStream)
        {
            ReApplyCalled = true;
            EventsApplied = eventStream;
            base.ReApply(eventStream);
        }
    }
}
