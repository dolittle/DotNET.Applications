using System;
using doLittle.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class SimpleEvent : Event
    {
        public string Content { get; set; }
    }
}