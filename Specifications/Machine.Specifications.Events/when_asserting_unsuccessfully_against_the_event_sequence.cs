// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ReSharper disable CA1707

using System;
using Dolittle.Machine.Specifications.Events.given;
using Machine.Specifications;

namespace Dolittle.Machine.Specifications.Events
{
    [Subject("Asserting against the Uncommitted Event Stream")]
    public class when_asserting_unsuccessfully_against_the_event_sequence : an_aggregate_root_with_uncommitted_events
    {
        static Exception an_event_is_not_in_the_stream;
        static Exception another_event_is_at_the_beginning;
        static Exception an_event_is_at_the_end;
        static Exception an_event_is_at_sequence_number_2;
        static Exception unused_event_is_in_the_stream;

        Because of = () =>
        {
            an_event_is_not_in_the_stream = Catch.Exception(() => aggregate_root.ShouldNotHaveEvent<AnEvent>());
            another_event_is_at_the_beginning = Catch.Exception(() => aggregate_root.ShouldHaveEvent<AnotherEvent>().AtBeginning());
            an_event_is_at_the_end = Catch.Exception(() => aggregate_root.ShouldHaveEvent<AnEvent>().AtEnd());
            an_event_is_at_sequence_number_2 = Catch.Exception(() => aggregate_root.ShouldHaveEvent<AnEvent>().AtSequenceNumber(2));
            unused_event_is_in_the_stream = Catch.Exception(() => aggregate_root.ShouldHaveEvent<UnusedEvent>().InStream());
        };

        It should_not_not_have_an_event_in_the_stream = () => an_event_is_not_in_the_stream.ShouldBeOfExactType<SpecificationException>();
        It should_not_have_another_event_at_the_beginning = () => another_event_is_at_the_beginning.ShouldBeOfExactType<SpecificationException>();
        It should_not_have_an_event_at_the_end = () => an_event_is_at_the_end.ShouldBeOfExactType<SpecificationException>();
        It should_have_an_event_at_sequence_number_2 = () => an_event_is_at_sequence_number_2.ShouldBeOfExactType<SpecificationException>();
        It should_not_have_unused_event_in_the_stream = () => unused_event_is_in_the_stream.ShouldBeOfExactType<SpecificationException>();
    }
}