// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Machine.Specifications;
using Moq;
using Newtonsoft.Json;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter.given
{
    public class all_dependencies
    {
        protected static ISerializer serializer;
        protected static Mock<IArtifactTypeMap> artifact_type_map;

        Establish context = () =>
        {
            var providers = new JsonConverter[]
            {
                new ConceptConverter(),
                new ConceptDictionaryConverter()
            };
            var converterProvider = new Mock<ICanProvideConverters>();
            converterProvider.Setup(_ => _.Provide()).Returns(providers);

            var converterProviders = new List<ICanProvideConverters>
            {
                converterProvider.Object
            };
            var converters = new Mock<IInstancesOf<ICanProvideConverters>>();
            converters.Setup(_ => _.GetEnumerator()).Returns(converterProviders.GetEnumerator());

            serializer = new Serializer(converters.Object);

            artifact_type_map = new Mock<IArtifactTypeMap>();
        };
    }
}