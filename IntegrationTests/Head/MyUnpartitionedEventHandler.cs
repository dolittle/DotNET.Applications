// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Handling;

namespace Head
{
    [EventHandler("e2cf3586-6bdc-4824-9a09-7fc1b5a0bb0e"), NotPartitioned]
    public class MyUnpartitionedEventHandler : ICanHandleEvents
    {
        public Task Handle(MyEvent @event, EventContext context)
        {
            if (@event.Fail) throw new Exception("Failed");
            return Task.CompletedTask;
        }
    }
}