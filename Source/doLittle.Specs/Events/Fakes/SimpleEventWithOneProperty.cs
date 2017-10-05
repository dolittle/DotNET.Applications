using System;
using doLittle.Events;

namespace doLittle.Specs.Events.Fakes
{
    public class SimpleEventWithOneProperty : Event
    {
        public string SomeString { get; set; }
    }
}