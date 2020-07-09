// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Domain;
using Dolittle.Events;

namespace Head
{
    public class MyAggregate : AggregateRoot
    {
        public MyAggregate(EventSourceId eventSourceId) : base(eventSourceId) {}
    }
}
