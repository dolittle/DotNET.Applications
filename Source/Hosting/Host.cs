/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;
using System.Threading;
using doLittle.Applications;
using doLittle.Assemblies;
using doLittle.Configuration;
using doLittle.DependencyInversion;
using doLittle.Logging;
using Microsoft.Extensions.Logging;

namespace doLittle.Hosting
{
    /// <summary>
    /// Represents a simple host
    /// </summary>
    public class Host : IHost
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Host"/>
        /// </summary>
        public Host(IApplication application)
        {
            
            var loggerFactory = new LoggerFactory();
            var logAppenders = doLittle.Logging.Bootstrap.EntryPoint.Initialize(loggerFactory);
            var logger = new Logger(logAppenders);

            var assemblies = doLittle.Assemblies.Bootstrap.EntryPoint.Initialize(logger);
            var typeFinder = doLittle.Types.Bootstrap.EntryPoint.Initialize(assemblies);

            IContainer container = null;

            var bindings = new[] {
                new BindingBuilder(Binding.For(typeof(IAssemblies))).To(assemblies).Build(),
                new BindingBuilder(Binding.For(typeof(Logging.ILogger))).To(logger).Build(),
                new BindingBuilder(Binding.For(typeof(IApplication))).To(application).Build(),
                new BindingBuilder(Binding.For(typeof(IHost))).To(this).Build(),
                new BindingBuilder(Binding.For(typeof(IContainer))).To(() => container).Build()
            };

            Container = container = doLittle.DependencyInversion.Bootstrap.EntryPoint.Initialize(assemblies, typeFinder, bindings);
        }

        /// <inheritdoc/>
        public IContainer Container { get; }

        /// <summary>
        /// Create a <see cref="IHostBuilder"/> for building configuration for the host
        /// </summary>
        /// <returns><see cref="IHostBuilder"/> to build on</returns>
        public static IHostBuilder CreateBuilder()
        {
            return CreateBuilder(ApplicationName.NotSet);
        }

        /// <summary>
        /// Create a <see cref="IHostBuilder"/> for building configuration for the host
        /// </summary>
        /// <param name="applicationName"><see cref="ApplicationName">Name of application</see></param>
        /// <returns><see cref="IHostBuilder"/> to build on</returns>
        public static IHostBuilder CreateBuilder(ApplicationName applicationName)
        {
            BoundedContextName boundedContextName = BoundedContextName.NotSet;

            if( applicationName == ApplicationName.NotSet )
            {
                var config = JsonFile.GetConfig();
                applicationName = config.Application;
                boundedContextName = config.BoundedContext;
            }
            
            if( applicationName == ApplicationName.NotSet )
                throw new ApplicationNameNotSet();

            IApplicationBuilder applicationBuilder = new ApplicationBuilder(applicationName);
            if( boundedContextName != BoundedContextName.NotSet )
                applicationBuilder = applicationBuilder.PrefixLocationsWith(new BoundedContext(boundedContextName));

            return new HostBuilder(applicationBuilder);
        }

        /// <inheritdoc/>
        public void Run()
        {
            Thread.Sleep(Timeout.Infinite);
        }
    }
}