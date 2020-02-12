// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling.for_EventHandlers
{
    public class MyEventHandler : ICanHandleEvents
    {
        public static Task Handle(MyFifthEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MyFirstEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MySecondEvent @event)
        {
            return Task.CompletedTask;
        }

        static Task Handle(MyFourthEvent @event)
        {
            return Task.CompletedTask;
        }

        Task Handle(MyThirdEvent @event)
        {
            return Task.CompletedTask;
        }
   }
}