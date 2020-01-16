// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;
using Dolittle.Runtime.Events;

namespace EventSourcing
{
    public class MyAggregate : AggregateRoot
    {
        public MyAggregate(EventSourceId eventSourceId)
            : base(eventSourceId)
        {
        }

        public void DoStuff()
        {
            Apply(new MyEvent());
        }
    }
}
