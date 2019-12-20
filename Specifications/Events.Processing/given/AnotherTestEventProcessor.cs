// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events;

namespace Dolittle.Events.Processing.given
{
    public class AnotherTestEventProcessor : ICanProcessEvents
    {
        [EventProcessor("5f752ea3-eb93-47db-9837-f1aa25f6806c")]
        public void Process1(MyEvent @event)
        {
        }

        [EventProcessor("8038aa08-4c25-4c11-a243-eac7febeb971")]
        public void Process2(MyEvent @event, EventSourceId id)
        {
        }

        [EventProcessor("bfaf129c-f8e4-4dc0-857d-338839be08c9")]
        public void Process3(MyEvent @event, EventMetadata metadata)
        {
        }
    }
}