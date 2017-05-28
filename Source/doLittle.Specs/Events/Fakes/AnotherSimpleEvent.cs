using doLittle.Events;

namespace doLittle.Specs.Events.Fakes
{
    public class AnotherSimpleEvent : Event
    {
        public AnotherSimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {}

        public string Content { get; set; }
    }
}