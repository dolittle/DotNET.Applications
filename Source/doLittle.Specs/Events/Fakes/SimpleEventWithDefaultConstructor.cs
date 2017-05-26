using Bifrost.Events;

namespace Bifrost.Specs.Events.Fakes
{
    public class SimpleEventWithDefaultConstructor : IEvent
    {
        public EventSourceId EventSourceId { get; set; }
    }
}