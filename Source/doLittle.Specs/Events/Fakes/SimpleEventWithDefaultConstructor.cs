using doLittle.Events;

namespace doLittle.Specs.Events.Fakes
{
    public class SimpleEventWithDefaultConstructor : IEvent
    {
        public EventSourceId EventSourceId { get; set; }
    }
}