// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Execution;
using Dolittle.Runtime.Commands;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandCoordinator"/>.
    /// </summary>
    public class CommandCoordinator : ICommandCoordinator
    {
        readonly Runtime.Commands.Coordination.ICommandCoordinator _runtimeCommandCoordinator;
        readonly ICommandToCommandRequestConverter _converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCoordinator"/> class.
        /// </summary>
        /// <param name="runtimeCommandCoordinator">Underlying <see cref="Runtime.Commands.Coordination.ICommandCoordinator"/>.</param>
        /// <param name="converter"><see cref="ICommandToCommandRequestConverter"/> for converting to a request.</param>
        public CommandCoordinator(Runtime.Commands.Coordination.ICommandCoordinator runtimeCommandCoordinator, ICommandToCommandRequestConverter converter)
        {
            _runtimeCommandCoordinator = runtimeCommandCoordinator;
            _converter = converter;
        }

        /// <inheritdoc/>
        public CommandResult Handle(ICommand command)
        {
            var correlationId = CorrelationId.New();
            var request = _converter.Convert(correlationId, command);
            return _runtimeCommandCoordinator.Handle(request);
        }
    }
}