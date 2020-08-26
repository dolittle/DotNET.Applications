// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Machine.Specifications.Events.given;
using Machine.Specifications;

namespace Dolittle.Machine.Specifications.Events
{
    [Subject("Asserting against the Uncommitted Event Stream")]
    public class when_asserting_unsuccessfully_against_the_event : an_aggregate_root_with_uncommitted_events
    {
        static Exception an_event_at_beginning_with_incorrect_values;
        static Exception another_event_at_end_with_incorrect_values;

        Because of = () =>
        {
            an_event_at_beginning_with_incorrect_values =
                Catch.Exception(
    () => aggregate_root
                        .ShouldHaveEvent<AnEvent>()
                        .AtBeginning()
                        .WithValues(
                            _ => _.AString == fourth_event.AString,
                            _ => _.AnInt == fourth_event.AnInt));
            another_event_at_end_with_incorrect_values =
                Catch.Exception(
        () => aggregate_root
                                .ShouldHaveEvent<AnotherEvent>()
                                .AtEnd()
                                .WithValues(
                                    _ => _.AString == first_event.AString,
                                    _ => _.AnInt == first_event.AnInt));
        };

        It should_not_assert_an_event_with_incorrect_values = () => an_event_at_beginning_with_incorrect_values.ShouldBeOfExactType<SpecificationException>();
        It should_not_assert_another_event_with_incorrect_values = () => another_event_at_end_with_incorrect_values.ShouldBeOfExactType<SpecificationException>();
    }
}