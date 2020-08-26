// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Machine.Specifications.Events.given
{
    public class an_aggregate_root_with_uncommitted_events
    {
        protected static AnAggregateRoot aggregate_root;
        protected static AnEvent first_event;
        protected static AnotherEvent fourth_event;
        static AnEvent second_event;
        static AnotherEvent third_event;

        Establish context = () =>
        {
            aggregate_root = new AnAggregateRoot(Guid.NewGuid());
            first_event = new AnEvent("first", 100);
            second_event = new AnEvent("second", 200);
            third_event = new AnotherEvent("third", 300);
            fourth_event = new AnotherEvent("fourth", 400);

            aggregate_root.DoStuff(first_event.AnInt, first_event.AString);
            aggregate_root.DoStuff(second_event.AnInt, second_event.AString);
            aggregate_root.DoMoreStuff(third_event.AnInt, third_event.AString);
            aggregate_root.DoMoreStuff(fourth_event.AnInt, fourth_event.AString);
        };
    }
}