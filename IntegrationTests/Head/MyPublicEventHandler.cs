// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Events.Handling.EventHorizon;

namespace Head
{
    [EventHandler("77ab759e-89b2-48c3-bedd-6b7327847f07")]
    [Scope("de594e7b-d160-44e4-9901-ae84fc70424a")]
    public class MyPublicEventHandler : ICanHandleExternalEvents
    {
        public Task Handle(MyPublicEvent @event, EventContext context)
        {
            if (@event.Fail) throw new Exception("Failed");
            return Task.CompletedTask;
        }
    }
}