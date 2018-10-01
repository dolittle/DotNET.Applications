/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class SimpleStatelessAggregateRoot : AggregateRoot
    {
        public SimpleStatelessAggregateRoot(EventSourceId id)
            : base(id)
        {
        }

        public bool ReApplyCalled = false;
        public CommittedEvents EventsApplied;

        public override void ReApply(CommittedEvents committedEvents)
        {
            ReApplyCalled = true;
            EventsApplied = committedEvents;
            base.ReApply(committedEvents);
        }
    }
}
