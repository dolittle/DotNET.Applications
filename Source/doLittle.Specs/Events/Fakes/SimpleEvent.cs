using doLittle.Events;

namespace doLittle.Specs.Events.Fakes
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {
            
        }

        public string Content { get; set; }
    }
}