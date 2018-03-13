/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Transactions;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandCoordinator"/>
    /// </summary>
    public class CommandCoordinator : ICommandCoordinator
    {
        readonly Dolittle.Runtime.Commands.Coordination.ICommandCoordinator _runtimeCommandCoordinator;
        readonly ICommandToCommandRequestConverter _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandCoordinator"/>
        /// </summary>
        /// <param name="runtimeCommandCoordinator">Underlying <see cref="Dolittle.Runtime.Commands.Coordination.ICommandCoordinator"/></param>
        /// <param name="converter"><see cref="ICommandToCommandRequestConverter"/> for converting to a request</param>
        public CommandCoordinator(Dolittle.Runtime.Commands.Coordination.ICommandCoordinator runtimeCommandCoordinator, ICommandToCommandRequestConverter converter)
        {
            _runtimeCommandCoordinator = runtimeCommandCoordinator;
            _converter = converter;
        }

        /// <inheritdoc/>
        public CommandResult Handle(ICommand command)
        {
            var correlationId = TransactionCorrelationId.New();
            var request = _converter.Convert(correlationId, command);
            var result = _runtimeCommandCoordinator.Handle(request);
            return result;
        }
    }
}