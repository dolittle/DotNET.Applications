// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Machine.Specifications.Events.given;
using global::Machine.Specifications;

namespace Dolittle.Machine.Specifications.Events
{
    [Subject("Asserting against the Uncommitted Event Stream")]
    public class when_asserting_successfully_against_the_event : an_aggregate_root_with_uncommitted_events
    {
        It should_have_an_event_at_the_beginning_with_correct_values
                = () => aggregate_root.ShouldHaveEvent<AnEvent>().AtBeginning()
                                        .WithValues(_ => _.AString == first_event.AString, _ => _.AnInt == first_event.AnInt);

        It should_have_another_event_at_the_end_where_the_values_are_correct
                = () => aggregate_root
                        .ShouldHaveEvent<AnotherEvent>()
                        .AtEnd()
                        .Where(
                            _ => _.AString.ShouldEqual(fourth_event.AString),
                            _ => _.AnInt.ShouldEqual(fourth_event.AnInt));
    }
}