using System;
using doLittle.Domain;

namespace doLittle.Specs.Events.Fakes
{
    public class StatelessAggregatedRoot : AggregateRoot
    {
        public StatelessAggregatedRoot(Guid id) : base(id) {}
    }
}