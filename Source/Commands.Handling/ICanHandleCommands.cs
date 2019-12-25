// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Marker interface for command handlers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A command handler must then implement a Handle method that takes the
    /// specific <see cref="ICommand">command</see> you want to be handled.
    /// </para>
    /// <para>
    /// The system will automatically detect your command handlers and methods
    /// and call it automatically when a <see cref="ICommand">command</see>
    /// comes into the system.
    /// </para>
    /// </remarks>
    public interface ICanHandleCommands
    {
    }
}
