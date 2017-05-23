using System;
using Bifrost.Events;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {
            
        }

        public string Content { get; set; }
    }
}