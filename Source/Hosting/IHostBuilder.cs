/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Applications;

namespace doLittle.Hosting
{
    /// <summary>
    /// Defines abstract builder for building a <see cref="IHost"/>
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// Start building the <see cref="IApplication"/>
        /// </summary>
        /// <param name="callback"></param>
        /// <returns><see cref="IHostBuilder"/> to continue building</returns>
        IHostBuilder Application(Func<IApplicationBuilder, IApplicationBuilder> callback);

        /// <summary>
        /// Build the <see cref="IHost"/>
        /// </summary>
        /// <returns>An instance of a <see cref="IHost"/></returns>
        IHost Build();
    }
}