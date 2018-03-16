using System;
using Dolittle.Events;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }
}