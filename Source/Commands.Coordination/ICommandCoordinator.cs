/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Dolittle.Runtime.Commands;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Defines the coordinator commands
    /// </summary>
    public interface ICommandCoordinator
    {
        /// <summary>
        /// Handles a <see cref="ICommand"/> and returns the <see cref="CommandResult"/> from handling it
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to handle</param>
        /// <returns>The <see cref="CommandResult">result</see></returns>
        CommandResult   Handle(ICommand command);
    }
}
