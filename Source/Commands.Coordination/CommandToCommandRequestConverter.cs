/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Applications;
using Dolittle.Commands;
using Dolittle.Reflection;
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
        readonly IApplicationArtifactIdentifierToTypeMaps _aaiToTypeMaps;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandToCommandRequestConverter"/>
        /// </summary>
        /// <param name="aaiToTypeMaps">The <see cref="IApplicationArtifactIdentifierToTypeMaps"/> for mapping artifacts to types and oposite</param>
        /// <param name="serializer"></param>
        public CommandToCommandRequestConverter(IApplicationArtifactIdentifierToTypeMaps aaiToTypeMaps, ISerializer serializer)
        {
            _aaiToTypeMaps = aaiToTypeMaps;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public CommandRequest Convert(TransactionCorrelationId correlationId, ICommand command)
        {
            var commandAsJson = _serializer.ToJson(command);
            var commandAsDictionary = _serializer.GetKeyValuesFromJson(commandAsJson);
            //var commandAsDictionary = command.ToDictionary();
            var applicationArtifactIdentifier = _aaiToTypeMaps.Map(command);
            var commandRequest = new CommandRequest(correlationId, applicationArtifactIdentifier, commandAsDictionary);
            return commandRequest;
        }
    }
}