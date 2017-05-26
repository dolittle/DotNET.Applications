using Bifrost.Events;

namespace Bifrost.Specs.Commands
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {
            
        }

        public string Content { get; set; }
    }
}