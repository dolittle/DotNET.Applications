/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Commands;
using doLittle.Runtime.Commands;
using doLittle.Runtime.Transactions;

namespace doLittle.Commands.Coordination
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandCoordinator"/>
    /// </summary>
    public class CommandCoordinator : ICommandCoordinator
    {
        readonly doLittle.Runtime.Commands.Coordination.ICommandCoordinator _runtimeCommandCoordinator;
        readonly ICommandToCommandRequestConverter _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandCoordinator"/>
        /// </summary>
        /// <param name="runtimeCommandCoordinator">Underlying <see cref="doLittle.Runtime.Commands.Coordination.ICommandCoordinator"/></param>
        /// <param name="converter"><see cref="ICommandToCommandRequestConverter"/> for converting to a request</param>
        public CommandCoordinator(doLittle.Runtime.Commands.Coordination.ICommandCoordinator runtimeCommandCoordinator, ICommandToCommandRequestConverter converter)
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