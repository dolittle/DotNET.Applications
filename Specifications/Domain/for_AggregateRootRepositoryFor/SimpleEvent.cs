using System;
using Dolittle.Events;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }
}