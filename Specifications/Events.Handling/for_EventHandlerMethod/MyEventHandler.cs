// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling.for_EventHandlerMethod
{
    public class MyEventHandler : ICanHandleEvents
    {
        public MyEvent EventPassed { get; private set; }

        public void SyncHandle(MyEvent @event)
        {
        }

        public Task AsyncHandle(MyEvent @event)
        {
            EventPassed = @event;
            return Task.CompletedTask;
        }

        public async void AsyncVoidHandle(MyEvent @event)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}