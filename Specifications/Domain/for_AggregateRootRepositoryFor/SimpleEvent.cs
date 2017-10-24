using System;
using doLittle.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }
}