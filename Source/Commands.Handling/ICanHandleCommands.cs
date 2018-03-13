/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Marker interface for command handlers.
    /// </summary>
    /// <remarks>
    /// A command handler must then implement a Handle method that takes the
    /// specific <see cref="ICommand">command</see> you want to be handled.
    ///
    /// The system will automatically detect your command handlers and methods
    /// and call it automatically when a <see cref="ICommand">command</see>
    /// comes into the system.
    /// </remarks>
    public interface ICanHandleCommands
    {
    }
}
