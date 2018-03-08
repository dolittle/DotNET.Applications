/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Commands;
using doLittle.Runtime.Commands;

namespace doLittle.Commands.Coordination
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
