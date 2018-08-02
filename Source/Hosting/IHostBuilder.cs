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
    /// Defines abstract builder for building a <see cref="IHost"/>
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// Build the <see cref="IHost"/>
        /// </summary>
        /// <returns>An instance of a <see cref="IHost"/></returns>
        IHost Build();

        /// <summary>
        /// Build the <see cref="IHost"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for logging</param>
        /// <returns>An instance of a <see cref="IHost"/></returns>
        IHost Build(ILoggerFactory loggerFactory);
    }
}