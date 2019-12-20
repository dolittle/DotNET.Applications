// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Artifacts;
using Machine.Specifications;

namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.when_creating
{
    [Subject(typeof(ProcessMethodEventProcessor), "Create")]
    public class an_invoker_for_the_event_and_metadata_signature : given.event_processors
    {
        static ProcessMethodEventProcessor result;

        Because of = () => result = new ProcessMethodEventProcessor(object_factory.Object, container.Object, Guid.NewGuid(), new Artifact(Guid.NewGuid(), 1), typeof(given.MyEvent), method_with_event_and_metadata, logger.Object);

        It should_create_the_process_method_event_processor = () => result.ShouldNotBeNull();
    }
}