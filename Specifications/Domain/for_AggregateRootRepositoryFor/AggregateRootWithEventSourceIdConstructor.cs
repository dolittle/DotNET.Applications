using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithEventSourceIdConstructor : AggregateRoot
    {
        public AggregateRootWithEventSourceIdConstructor(EventSourceId id) : base(id)
        {

        }
    }
}
