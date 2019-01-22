/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Machine.Specifications;
using Moq;
using Newtonsoft.Json;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter.given
{
    public class a_serializer
    {
        protected static ISerializer serializer;

        Establish context = () =>
        {
            var providers = new JsonConverter[] {
                new ConceptConverter(),
                new ConceptDictionaryConverter()
            };
            var converterProvider = new Mock<ICanProvideConverters>();
            converterProvider.Setup(_ => _.Provide()).Returns(providers);

            var converterProviders = new List<ICanProvideConverters> {
                converterProvider.Object
            };
            var converters = new Mock<IInstancesOf<ICanProvideConverters>>();
            converters.Setup(_ => _.GetEnumerator()).Returns(converterProviders.GetEnumerator());

            serializer = new Serializer(converters.Object);
        };
    }
}