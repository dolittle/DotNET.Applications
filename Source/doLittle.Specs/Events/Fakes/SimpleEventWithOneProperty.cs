using System;
using Bifrost.Events;

namespace Bifrost.Specs.Events.Fakes
{
    public class SimpleEventWithOneProperty : Event
    {
        public SimpleEventWithOneProperty() : base(Guid.NewGuid()) { }
        public SimpleEventWithOneProperty(Guid guid) : base(guid) {}        

        public string SomeString { get; set; }
    }
}