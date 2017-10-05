using System;
using doLittle.Specs.Events.Fakes;
using Machine.Specifications;

namespace doLittle.Specs.Events.for_Event
{
    public class when_comparing_events_that_are_same_type_with_different_content
    {
        static SimpleEventWithOneProperty first_event;
        static SimpleEventWithOneProperty second_event;
        static bool is_equal;

        Establish context = () =>
                                {
                                    first_event = new SimpleEventWithOneProperty() {SomeString = "Something"};
                                    second_event = new SimpleEventWithOneProperty() {SomeString = "Something Else"};
                                };

        Because of = () => is_equal = first_event.Equals(second_event);

        It should_not_be_considered_equal = () => is_equal.ShouldBeFalse();
    }
}