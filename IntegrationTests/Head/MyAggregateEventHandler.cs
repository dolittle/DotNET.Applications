// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Handling;

namespace Head
{
    [EventHandler("2a494827-841c-43c2-b163-238ac9314f4a")]
    public class MyAggregateEventHandler : ICanHandleEvents
    {
        public Task Handle(MyAggregateEvent @event, EventContext context)
        {
            if (@event.Fail) throw new Exception("Failed");
            return Task.CompletedTask;
        }
    }
}