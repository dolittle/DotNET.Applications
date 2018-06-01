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
        readonly IApplicationBuilder _applicationBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="HostBuilder"/>
        /// </summary>
        /// <param name="applicationBuilder"><see cref="IApplicationBuilder"/> to use for building <see cref="IApplication"/></param>
        public HostBuilder(IApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        /// <inheritdoc/>
        public IHostBuilder Application(Func<IApplicationBuilder, IApplicationBuilder> callback)
        {
            return new HostBuilder(callback(_applicationBuilder));
        }

        /// <inheritdoc/>
        public IHost Build()
        {
            return new Host(_applicationBuilder.Build());
        }


        /// <inheritdoc/>
        public IHost Build(ILoggerFactory loggerFactory)
        {
            return new Host(_applicationBuilder.Build(), loggerFactory);
        }
        
    }
}