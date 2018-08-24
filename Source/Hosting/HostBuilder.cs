/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications;
using Microsoft.Extensions.Logging;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Represents an implementation of <see cref="IHostBuilder"/>
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        /// <inheritdoc/>
        public IHost Build(bool skipBootProcedures=false)
        {
            return new Host();
        }

        /// <inheritdoc/>
        public IHost Build(ILoggerFactory loggerFactory, bool skipBootProcedures=false)
        {
            return new Host(loggerFactory, skipBootProcedures);
        }
        
    }
}