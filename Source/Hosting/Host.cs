/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading;
using Dolittle.Assemblies;
using Dolittle.Bootstrapping;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Microsoft.Extensions.Logging;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Represents a simple host
    /// </summary>
    public class Host : IHost
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Host"/>
        /// </summary>
        public Host(ILoggerFactory loggerFactory = null, bool skipBootProcedures=false)
        {
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();
            var logAppenders = Dolittle.Logging.Bootstrap.EntryPoint.Initialize(loggerFactory);
            var logger = new Logger(logAppenders);

            var assemblies = Dolittle.Assemblies.Bootstrap.EntryPoint.Initialize(logger);
            var typeFinder = Dolittle.Types.Bootstrap.EntryPoint.Initialize(assemblies);


            var bindings = new[] {
                new BindingBuilder(Binding.For(typeof(IAssemblies))).To(assemblies).Build(),
                new BindingBuilder(Binding.For(typeof(Logging.ILogger))).To(logger).Build(),
                new BindingBuilder(Binding.For(typeof(IHost))).To(this).Build(),
            };

            var result = Dolittle.DependencyInversion.Bootstrap.Boot.Start(assemblies, typeFinder, logger, bindings);
            Container = result.Container;

            if( !skipBootProcedures ) Bootstrapper.Start(Container);
        }

        /// <inheritdoc/>
        public IContainer Container { get; }

        /// <summary>
        /// Create a <see cref="IHostBuilder"/> for building configuration for the host
        /// </summary>
        /// <returns><see cref="IHostBuilder"/> to build on</returns>
        public static IHostBuilder CreateBuilder()
        {
            return new HostBuilder();
        }

        /// <inheritdoc/>
        public void Run()
        {
            Thread.Sleep(Timeout.Infinite);
        }
    }
}