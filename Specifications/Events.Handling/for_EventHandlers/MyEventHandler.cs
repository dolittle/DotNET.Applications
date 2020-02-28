// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling.for_EventHandlers
{
    public class MyEventHandler : ICanHandleEvents
    {
        public static Task Handle(MyFifthEvent @event, EventContext context)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MyFirstEvent @event, EventContext context)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MySecondEvent @event, EventContext context)
        {
            return Task.CompletedTask;
        }

        static Task Handle(MyFourthEvent @event, EventContext context)
        {
            return Task.CompletedTask;
        }

        Task Handle(MyThirdEvent @event, EventContext context)
        {
            return Task.CompletedTask;
        }
   }
}