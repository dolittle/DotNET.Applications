// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Dolittle.Events.Handling.for_EventHandlerMethod
{
    public class MyAsyncVoidEventHandler : ICanHandleEvents
    {
        public MyEvent EventPassed { get; private set; }

        public async void Handle(MyEvent @event)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}