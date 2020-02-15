// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using Dolittle.Artifacts;
using Dolittle.Protobuf;
using Dolittle.Serialization.Json;
using Google.Protobuf.WellKnownTypes;
using Machine.Specifications;
using Moq;
using grpcArtifacts = contracts::Dolittle.Runtime.Artifacts;
using grpcEvents = contracts::Dolittle.Runtime.Events;
using It = Machine.Specifications.It;

namespace Dolittle.Events.for_EventConverter
{
    public class when_converting_from_protobuf
    {
        static Mock<IArtifactTypeMap> artifact_type_map;
        static Mock<ISerializer> serializer;
        static EventConverter converter;

        static CommittedEvent result;
        static grpcEvents.CommittedEvent input;

        static Guid artifact;
        static int generation;
        static MyEvent the_event;
        static DateTimeOffset occurred;

        Establish context = () =>
        {
            artifact_type_map = new Mock<IArtifactTypeMap>();
            serializer = new Mock<ISerializer>();

            converter = new EventConverter(artifact_type_map.Object, serializer.Object);

            artifact = Guid.NewGuid();
            generation = 42;
            occurred = DateTimeOffset.UtcNow;
            the_event = new MyEvent();

            input = new grpcEvents.CommittedEvent
            {
                Type = new grpcArtifacts.Artifact
                {
                    Id = artifact.ToProtobuf(),
                    Generation = generation
                },
                Occurred = Timestamp.FromDateTimeOffset(occurred),
                CorrelationId = Guid.NewGuid().ToProtobuf(),
                Microservice = Guid.NewGuid().ToProtobuf(),
                Tenant = Guid.NewGuid().ToProtobuf(),
                Cause = new grpcEvents.Cause
                {
                    Type = (int)CauseType.Command,
                    Position = 0
                },
                Content = "{\"someProperty\":42}"
            };

            artifact_type_map.Setup(_ => _.GetTypeFor(new Artifact(artifact, generation))).Returns(typeof(MyEvent));
            serializer.Setup(_ => _.FromJson(typeof(MyEvent), input.Content, SerializationOptions.CamelCase)).Returns(the_event);
        };

        Because of = () => result = converter.ToSDK(input);

        It should_hold_the_event = () => result.Event.ShouldEqual(the_event);
        It should_the_correct_timestamp = () => result.Occurred.ShouldEqual(occurred);
    }
}