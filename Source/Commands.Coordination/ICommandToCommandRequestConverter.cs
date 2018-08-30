/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.Execution;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Defines a system that is capable of converting from <see cref="ICommand"/> instances to <see cref="CommandRequest"/>
    /// </summary>
    public interface ICommandToCommandRequestConverter
    {
        /// <summary>
        /// Convert a <see cref="ICommand"/> to <see cref="CommandRequest"/>
        /// </summary>
        /// <param name="correlationId">The <see cref="CorrelationId">correlation id</see> of the request</param>
        /// <param name="command"><see cref="ICommand"/> to convert</param>
        /// <returns>Converted <see cref="CommandRequest"/></returns>
        CommandRequest Convert(CorrelationId correlationId, ICommand command);
    }
}