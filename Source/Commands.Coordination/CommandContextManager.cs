// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using Dolittle.Execution;
using Dolittle.Logging;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents a <see cref="ICommandContextManager">Command context manager</see>.
    /// </summary>
    public class CommandContextManager : ICommandContextManager
    {
        static readonly AsyncLocal<ICommandContext> _currentContext = new AsyncLocal<ICommandContext>();

        readonly ICommandContextFactory _factory;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextManager"/> class.
        /// </summary>
        /// <param name="factory">A <see cref="ICommandContextFactory"/> to use for building an <see cref="ICommandContext"/>.</param>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public CommandContextManager(ICommandContextFactory factory, IExecutionContextManager executionContextManager, ILogger logger)
        {
            _factory = factory;
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool HasCurrent => _currentContext.Value != null;

        /// <summary>
        /// Reset context.
        /// </summary>
        public static void ResetContext() => _currentContext.Value = null;

        /// <inheritdoc/>
        public ICommandContext GetCurrent()
        {
            if (!HasCurrent)
            {
                throw new NoEstablishedCommandContext();
            }

            return _currentContext.Value;
        }

        /// <inheritdoc/>
        public ICommandContext EstablishForCommand(CommandRequest command)
        {
            _logger.Debug("Establishing command context for command '{CommandType}' with correlation '{CorrelationId}'", command.Type, command.CorrelationId);
            if (!IsInContext(command))
            {
                _executionContextManager.CurrentFor(_executionContextManager.Current.Tenant, command.CorrelationId);
                _currentContext.Value = _factory.Build(command);
            }
            else
            {
                _executionContextManager.CurrentFor(_currentContext.Value.ExecutionContext);
            }

            return _currentContext.Value;
        }

        static bool IsInContext(CommandRequest command) => _currentContext.Value?.Command.Equals(command) == true;
    }
}