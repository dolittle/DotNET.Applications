/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Dolittle.Runtime.Commands;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Defines a converter that can convert between a <see cref="ICommand"/> and a <see cref="CommandRequest"/>
    /// </summary>
    public interface ICommandRequestToCommandConverter
    {
        /// <summary>
        /// Convert a <see cref="CommandRequest"/> to a <see cref="ICommand"/> instance of the correct type
        /// </summary>
        /// <param name="request"><see cref="CommandRequest"/> to convert from</param>
        /// <returns><see cref="ICommand"/> instance of the correct type</returns>
        ICommand Convert(CommandRequest request);
    }
}
