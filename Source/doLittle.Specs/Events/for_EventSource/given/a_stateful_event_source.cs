using Bifrost.Events;
using Machine.Specifications;
using Moq;
using System;
using Bifrost.Specs.Events.Fakes;

namespace Bifrost.Specs.Events.for_EventSource.given
{
    public class a_stateful_event_source : all_dependencies
	{
		protected static StatefulAggregatedRoot event_source;
		protected static EventSourceId event_source_id;
		protected static IEvent @event;
        protected static Mock<IEventEnvelope> event_envelope;

		Establish context = () =>
				{
					event_source_id = Guid.NewGuid();
					event_source = new StatefulAggregatedRoot(event_source_id);
                    event_envelope = new Mock<IEventEnvelope>();
                    event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
                    event_envelopes.Setup(e => e.CreateFrom(event_source, Moq.It.IsAny<IEvent>(), Moq.It.IsAny<EventSourceVersion>())).Returns(event_envelope.Object);
				};
	}
}