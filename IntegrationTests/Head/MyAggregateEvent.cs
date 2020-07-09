// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;

namespace Head
{
    public class MyAggregateEvent : IEvent
    {
        public string UniqueIdentifier { get; set; }
        public bool Fail { get; set; } = false;
    }
}
