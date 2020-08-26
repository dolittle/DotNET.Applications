// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Serialization.Json;

namespace Dolittle.Commands
{
    /// <summary>
    /// Represents async implementation of <see cref="ICommandToCommandRequestConverter"/>.
    /// </summary>
    public class CommandToCommandRequestConverter : ICommandToCommandRequestConverter
    {
        readonly IArtifactTypeMap _artifactsTypeMap;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandToCommandRequestConverter"/> class.
        /// </summary>
        /// <param name="artifactsTypeMap">The <see cref="IArtifactTypeMap"/> for identifying artifacts.</param>
        /// <param name="serializer"><see cref="ISerializer"/>.</param>
        public CommandToCommandRequestConverter(IArtifactTypeMap artifactsTypeMap, ISerializer serializer)
        {
            _artifactsTypeMap = artifactsTypeMap;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public CommandRequest Convert(CorrelationId correlationId, ICommand command)
        {
            var commandAsJson = _serializer.ToJson(command);
            var commandAsDictionary = _serializer.GetKeyValuesFromJson(commandAsJson);
            var artifact = _artifactsTypeMap.GetArtifactFor(command.GetType());
            return new CommandRequest(correlationId, artifact.Id, artifact.Generation, commandAsDictionary);
        }
    }
}
