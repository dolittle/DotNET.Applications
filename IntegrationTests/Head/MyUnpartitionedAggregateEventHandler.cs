// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Handling;

namespace Head
{
    [EventHandler("bb3d3253-0f55-4710-b278-64e2fc29646c"), NotPartitioned]
    public class MyUnpartitionedAggregateEventHandler : ICanHandleEvents
    {
        public Task Handle(MyAggregateEvent @event, EventContext context)
        {
            if (@event.Fail) throw new Exception("Failed");
            return Task.CompletedTask;
        }
    }
}