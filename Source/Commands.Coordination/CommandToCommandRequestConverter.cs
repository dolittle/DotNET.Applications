/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Transactions;
using Dolittle.Serialization.Json;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents async implementation of <see cref="ICommandToCommandRequestConverter"/>
    /// </summary>
    public class CommandToCommandRequestConverter : ICommandToCommandRequestConverter
    {
        readonly IArtifactTypeMap _artifactsTypeMap;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandToCommandRequestConverter"/>
        /// </summary>
        /// <param name="artifactsTypeMap">The <see cref="IArtifactTypeMap"/> for identifying artifacts</param>
        /// <param name="serializer"></param>
        public CommandToCommandRequestConverter(IArtifactTypeMap artifactsTypeMap, ISerializer serializer)
        {
            _artifactsTypeMap = artifactsTypeMap;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public CommandRequest Convert(TransactionCorrelationId correlationId, ICommand command)
        {
            var commandAsJson = _serializer.ToJson(command);
            var commandAsDictionary = _serializer.GetKeyValuesFromJson(commandAsJson);
            //var commandAsDictionary = command.ToDictionary();
            var artifact = _artifactsTypeMap.GetArtifactFor(command.GetType());
            //var commandRequest = new CommandRequest(correlationId, artifact.Id, artifact.Generation, commandAsDictionary);
            var commandRequest = new CommandRequest(correlationId, artifact, commandAsDictionary);
            return commandRequest;
        }
    }
}