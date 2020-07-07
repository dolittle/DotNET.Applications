// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter.given
{
    public class a_converter : all_dependencies
    {
        protected static ICommandRequestToCommandConverter converter;

        Establish context = () => converter = new CommandRequestToCommandConverter(artifact_type_map.Object, serializer, Mock.Of<ILogger>());
    }
}