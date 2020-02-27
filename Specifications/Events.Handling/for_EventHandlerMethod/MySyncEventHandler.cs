// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Handling.for_EventHandlerMethod
{
    public class MySyncEventHandler : ICanHandleEvents
    {
        public MyEvent EventPassed { get; private set; }

        public void Handle(MyEvent @event)
        {
        }
    }
}