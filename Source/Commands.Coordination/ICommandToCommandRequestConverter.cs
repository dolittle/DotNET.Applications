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
    /// Defines a system that is capable of converting from <see cref="ICommand"/> instances to <see cref="CommandRequest"/>
    /// </summary>
    public interface ICommandToCommandRequestConverter
    {
        /// <summary>
        /// Convert a <see cref="ICommand"/> to <see cref="CommandRequest"/>
        /// </summary>
        /// <param name="correlationId">The <see cref="TransactionCorrelationId">correlation id</see> of the request</param>
        /// <param name="command"><see cref="ICommand"/> to convert</param>
        /// <returns>Converted <see cref="CommandRequest"/></returns>
        CommandRequest Convert(TransactionCorrelationId correlationId, ICommand command);
    }
}