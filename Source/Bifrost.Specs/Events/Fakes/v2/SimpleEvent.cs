using Bifrost.Events;

namespace Bifrost.Specs.Events.Fakes.v2
{
    public class SimpleEvent : Fakes.SimpleEvent, IAmNextGenerationOf<Fakes.SimpleEvent>
    {
        public static string DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY = "2nd: DEFAULT";

        public string SecondGenerationProperty { get; set; }


        public SimpleEvent(EventSourceId  eventSourceId) : base(eventSourceId)
        {
            SecondGenerationProperty = DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY;
        }
    }
}