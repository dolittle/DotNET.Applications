using doLittle.Domain;
using doLittle.Runtime.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithEventSourceIdConstructor : AggregateRoot
    {
        public AggregateRootWithEventSourceIdConstructor(EventSourceId id) : base(id)
        {

        }
    }
}
