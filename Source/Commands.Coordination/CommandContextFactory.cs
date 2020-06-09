// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events;
using Dolittle.Events.Handling;
using Dolittle.Execution;
using Dolittle.Logging;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents a <see cref="ICommandContextFactory"/>.
    /// </summary>
    public class CommandContextFactory : ICommandContextFactory
    {
        readonly IEventStore _eventStore;
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventProcessingCompletion _eventHandlersWaiters;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextFactory"/> class.
        /// </summary>
        /// <param name="eventStore">The <see cref="IEventStore"/> to use for committing events.</param>
        /// <param name="executionContextManager">A <see cref="IExecutionContextManager"/> for getting execution context from.</param>
        /// <param name="eventHandlersWaiters"><see cref="IEventProcessingCompletion"/> for waiting on event handlers.</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging.</param>
        public CommandContextFactory(
            IEventStore eventStore,
            IExecutionContextManager executionContextManager,
            IEventProcessingCompletion eventHandlersWaiters,
            ILogger logger)
        {
            _eventStore = eventStore;
            _executionContextManager = executionContextManager;
            _eventHandlersWaiters = eventHandlersWaiters;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ICommandContext Build(CommandRequest command)
        {
            _logger.Debug("Building new command context for command {CommandType} with correlation {Correlation}", command.Type, command.CorrelationId);
            return new CommandContext(
                command,
                _executionContextManager.Current,
                _eventStore,
                _eventHandlersWaiters,
                _logger);
        }
    }
}