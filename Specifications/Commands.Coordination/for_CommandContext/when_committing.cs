// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using Dolittle.Collections;
using Dolittle.Events;
using Machine.Specifications;

namespace Dolittle.Commands.Coordination.for_CommandContext
{
    public class when_committing : given.a_command_context_for_a_simple_command_with_one_tracked_object_with_one_uncommitted_event
    {
        static IList<IEvent> events;

        Establish context = () =>
        {
            events = new List<IEvent>();
            event_store.Setup(e => e.CommitForAggregate(Moq.It.IsAny<UncommittedAggregateEvents>(), Moq.It.IsAny<CancellationToken>()))
                .Callback((UncommittedAggregateEvents uncommitted, CancellationToken _) => uncommitted.ForEach(events.Add));
        };

        Because of = () => command_context.Commit();

        It should_commit_on_the_event_store = () => events.ShouldNotBeEmpty();
        It should_commit_on_the_event_store_with_the_event_in_event_stream = () => events.ShouldContainOnly(uncommitted_event);
        It should_commit_aggregated_root = () => aggregated_root.CommitCalled.ShouldBeTrue();
    }
}
