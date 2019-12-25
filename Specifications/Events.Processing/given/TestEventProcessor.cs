// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Runtime.Events;

namespace Dolittle.Events.Processing.given
{
    public class TestEventProcessor : ICanProcessEvents
    {
        public int JustTheEventCalled { get; private set; }

        public int TheEventAndTheIdCalled { get; private set; }

        public int TheEventAndTheMetadataCalled { get; private set; }

        [EventProcessor("77ca7665-605f-4cda-8dbc-8b9640328e13")]
        public void JustTheEvent(MyEvent @event)
        {
            JustTheEventCalled++;
        }

        [EventProcessor("c3394c93-ef16-498d-8bc5-28254624f1df")]
        public void TheEventAndTheId(MyEvent @event, EventSourceId id)
        {
            TheEventAndTheIdCalled++;
        }

        [EventProcessor("a96c9899-e0ea-4fb0-9172-813d297dca0b")]
        public void TheEventAndTheMetadata(MyEvent @event, EventMetadata metadata)
        {
            TheEventAndTheMetadataCalled++;
        }

        public void AValidSignatureButNotMarkedAsAnEventProcessor(MyEvent @event)
        {
        }

        public void AnInvalidSignature()
        {
        }

        public MyEvent AnInvalidSignatureBecauseOfReturnType(MyEvent @event)
        {
            return @event;
        }
    }
}