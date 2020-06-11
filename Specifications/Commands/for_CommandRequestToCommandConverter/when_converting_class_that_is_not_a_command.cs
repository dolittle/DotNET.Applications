// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter
{
    public class when_converting_class_that_is_not_a_command : given.a_converter
    {
        static CorrelationId correlation_id;
        static Artifact identifier;
        static CommandRequest request;
        static Exception exception;

        Establish context = () =>
        {
            correlation_id = CorrelationId.New();
            identifier = Artifact.New();

            request = new CommandRequest(correlation_id, identifier.Id, identifier.Generation, new Dictionary<string, object>());

            artifact_type_map.Setup(_ => _.GetTypeFor(identifier)).Returns(typeof(complex_type));
        };

        Because of = () => exception = Catch.Exception(() => converter.Convert(request));

        It should_fail_because_artifact_is_not_a_command = () => exception.ShouldBeOfExactType<ArtifactIsNotCommand>();
    }
}