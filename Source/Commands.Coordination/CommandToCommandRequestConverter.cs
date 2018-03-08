/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Applications;
using doLittle.Commands;
using doLittle.Reflection;
using doLittle.Runtime.Commands;
using doLittle.Runtime.Transactions;

namespace doLittle.Commands.Coordination
{
    /// <summary>
    /// Represents async implementation of <see cref="ICommandToCommandRequestConverter"/>
    /// </summary>
    public class CommandToCommandRequestConverter : ICommandToCommandRequestConverter
    {
        readonly IApplicationArtifacts _applicationArtifacts;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandToCommandRequestConverter"/>
        /// </summary>
        /// <param name="applicationArtifacts">The <see cref="IApplicationArtifacts"/> for identifying artifacts</param>
        public CommandToCommandRequestConverter(IApplicationArtifacts applicationArtifacts)
        {
            _applicationArtifacts = applicationArtifacts;
        }

        /// <inheritdoc/>
        public CommandRequest Convert(TransactionCorrelationId correlationId, ICommand command)
        {
            var commandAsDictionary = command.ToDictionary();
            var applicationArtifactIdentifier = _applicationArtifacts.Identify(command);
            var commandRequest = new CommandRequest(correlationId, applicationArtifactIdentifier, commandAsDictionary);
            return commandRequest;
        }
    }
}