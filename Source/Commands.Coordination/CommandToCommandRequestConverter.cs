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
        readonly IApplicationArtifacts _applicationArtifacts;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandToCommandRequestConverter"/>
        /// </summary>
        /// <param name="applicationArtifacts">The <see cref="IApplicationArtifacts"/> for identifying artifacts</param>
        /// <param name="serializer"></param>
        public CommandToCommandRequestConverter(IApplicationArtifacts applicationArtifacts, ISerializer serializer)
        {
            _applicationArtifacts = applicationArtifacts;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public CommandRequest Convert(TransactionCorrelationId correlationId, ICommand command)
        {
            var commandAsJson = _serializer.ToJson(command);
            var commandAsDictionary = _serializer.GetKeyValuesFromJson(commandAsJson);
            //var commandAsDictionary = command.ToDictionary();
            var applicationArtifactIdentifier = _applicationArtifacts.Identify(command);
            var commandRequest = new CommandRequest(correlationId, applicationArtifactIdentifier, commandAsDictionary);
            return commandRequest;
        }
    }
}