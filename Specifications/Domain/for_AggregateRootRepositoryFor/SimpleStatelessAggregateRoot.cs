using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
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
