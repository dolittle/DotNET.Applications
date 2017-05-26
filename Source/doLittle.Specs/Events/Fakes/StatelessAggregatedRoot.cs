using System;
using Bifrost.Domain;

namespace Bifrost.Specs.Events.Fakes
{
    public class StatelessAggregatedRoot : AggregateRoot
    {
        public StatelessAggregatedRoot(Guid id) : base(id) {}
    }
}