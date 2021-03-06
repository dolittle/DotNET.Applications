// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Domain.for_AggregateRoot.given;
using Dolittle.Events;
using Machine.Specifications;

namespace Dolittle.Domain.for_AggregateRoot
{
    public class when_reapplying_events_to_a_wrong_event_source : given.committed_events_and_two_aggregate_roots
    {
        static CommittedAggregateEvents events;
        static Exception exception;

        Establish context = () => events = build_committed_events(Guid.Parse("aa81d146-0c45-4121-bce0-fcc547257ccd"), typeof(StatefulAggregateRoot), execution_context);

        Because of = () => exception = Catch.Exception(() => statefull_aggregate_root.ReApply(events));

        It should_throw_an_exception = () => exception.ShouldBeOfExactType<EventWasAppliedToOtherEventSource>();
        It should_be_at_version_three = () => statefull_aggregate_root.Version.ShouldEqual(AggregateRootVersion.Initial);
        It should_have_no_uncommitted_events = () => stateless_aggregate_root.UncommittedEvents.ShouldBeEmpty();
        It should_have_no_broken_rules = () => statefull_aggregate_root.BrokenRules.ShouldBeEmpty();
        It should_have_no_rule_set_evaulations = () => statefull_aggregate_root.RuleSetEvaluations.ShouldBeEmpty();
    }
}
