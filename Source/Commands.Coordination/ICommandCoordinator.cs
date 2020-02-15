// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Defines the coordinator of commands.
    /// </summary>
    public interface ICommandCoordinator
    {
        /// <summary>
        /// Handles a <see cref="ICommand"/> and returns the <see cref="CommandResult"/> from handling it.
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to handle.</param>
        /// <returns>The <see cref="CommandResult">result</see>.</returns>
        CommandResult Handle(ICommand command);
    }
}
