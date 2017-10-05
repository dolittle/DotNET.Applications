using System;
using doLittle.Specs.Events.Fakes;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Events.for_Event
{
    public class when_comparing_events_of_different_types
    {
        static SimpleEvent first_event;
        static AnotherSimpleEvent second_event;
        static bool is_equal;

        Establish context = () =>
                                {
                                    first_event = new SimpleEvent();
                                    second_event = new AnotherSimpleEvent();
                                };

        Because of = () => is_equal = first_event.Equals(second_event);

        It should_not_be_considered_equal = () => is_equal.ShouldBeFalse();
    }
}
