// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Events.Handling.EventHorizon;

namespace Head
{
    [EventHandler("80e1032c-6276-409e-961c-217010f84219")]
    [Scope("de594e7b-d160-44e4-9901-ae84fc70424a")]
    [NotPartitioned]
    public class MyUnpartitionedPublicEventHandler : ICanHandleExternalEvents
    {
        public Task Handle(MyPublicEvent @event, EventContext context)
        {
            if (@event.Fail) throw new Exception("Failed");
            return Task.CompletedTask;
        }
    }
}