using System;
using doLittle.Events;

namespace doLittle.Specs.Events.Fakes
{
    public class SimpleEventWithOneProperty : Event
    {
        public SimpleEventWithOneProperty() : base(Guid.NewGuid()) { }
        public SimpleEventWithOneProperty(Guid guid) : base(guid) {}        

        public string SomeString { get; set; }
    }
}