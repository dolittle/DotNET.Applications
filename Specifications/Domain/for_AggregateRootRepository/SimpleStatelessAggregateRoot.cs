using doLittle.Domain;
using doLittle.Runtime.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class SimpleStatelessAggregateRoot : AggregateRoot
    {
        public SimpleStatelessAggregateRoot(EventSourceId id)
            : base(id)
        {
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
