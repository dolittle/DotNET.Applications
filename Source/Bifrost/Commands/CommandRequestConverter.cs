/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandRequestConverter"/>
    /// </summary>
    public class CommandRequestConverter : ICommandRequestConverter
    {
        /// <inheritdoc/>
        public ICommand Convert(CommandRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
