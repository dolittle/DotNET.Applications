/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading;
using Dolittle.Assemblies;
using Dolittle.Bootstrapping;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Microsoft.Extensions.Logging;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Represents a simple host
    /// </summary>
    public class Host : IHost
    {
        readonly AsyncLocal<LoggingContext>  _currentLoggingContext = new AsyncLocal<LoggingContext>();
        IExecutionContextManager _executionContextManager;
        Dolittle.Execution.ExecutionContext _initialExecutionContext;

        LoggingContext GetCurrentLoggingContext()
        {
            Dolittle.Execution.ExecutionContext executionContext = null;

            if( _executionContextManager == null && Container != null )
                _executionContextManager = Container.Get<IExecutionContextManager>();

            if (LoggingContextIsSet())
            {
                if (_executionContextManager != null) SetLatestLoggingContext();
                return _currentLoggingContext.Value;
            }
            
            if( _executionContextManager != null ) executionContext = _executionContextManager.Current;
            else executionContext = _initialExecutionContext;

            var loggingContext = CreateLoggingContextFrom(executionContext);
            _currentLoggingContext.Value = loggingContext;

            return loggingContext;
        }
        bool LoggingContextIsSet() => 
            _currentLoggingContext != null && _currentLoggingContext.Value != null;

        void SetLatestLoggingContext() => 
            _currentLoggingContext.Value = CreateLoggingContextFrom(_executionContextManager.Current);
            
        
        LoggingContext CreateLoggingContextFrom(Dolittle.Execution.ExecutionContext executionContext) =>
            new LoggingContext {
                Application = executionContext.Application,
                BoundedContext = executionContext.BoundedContext,
                CorrelationId = executionContext.CorrelationId,
                Environment = executionContext.Environment,
                TenantId = executionContext.Tenant
            };
        
        /// <summary>
        /// Initializes a new instance of <see cref="Host"/>
        /// </summary>
        public Host(ILoggerFactory loggerFactory = null, bool skipBootProcedures=false)
        {
            _initialExecutionContext = ExecutionContextManager.SetInitialExecutionContext();
            
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();
            var logAppenders = Dolittle.Logging.Bootstrap.EntryPoint.Initialize(loggerFactory, GetCurrentLoggingContext);
            var logger = new Logger(logAppenders);

            var assemblies = Dolittle.Assemblies.Bootstrap.EntryPoint.Initialize(logger);
            var typeFinder = Dolittle.Types.Bootstrap.EntryPoint.Initialize(assemblies);
            Dolittle.Resources.Configuration.Bootstrap.EntryPoint.Initialize(typeFinder);
            
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