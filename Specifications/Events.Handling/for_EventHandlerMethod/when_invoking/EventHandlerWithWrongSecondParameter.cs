// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling.for_EventHandlerMethod.when_invoking
{
    public class EventHandlerWithWrongSecondParameter : ICanHandleEvents
    {
        public MyEvent EventPassed { get; private set; }

        public Task Handle(MyEvent @event, string something)
        {
            EventPassed = @event;
            return Task.CompletedTask;
        }
    }
}