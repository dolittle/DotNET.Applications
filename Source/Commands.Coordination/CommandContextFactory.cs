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
        readonly IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventProcessingCompletion _eventHandlersWaiters;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextFactory"/> class.
        /// </summary>
        /// <param name="uncommittedEventStreamCoordinator">A <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinator an <see cref="UncommittedEvents"/>.</param>
        /// <param name="executionContextManager">A <see cref="IExecutionContextManager"/> for getting execution context from.</param>
        /// <param name="eventHandlersWaiters"><see cref="IEventProcessingCompletion"/> for waiting on event handlers.</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging.</param>
        public CommandContextFactory(
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            IExecutionContextManager executionContextManager,
            IEventProcessingCompletion eventHandlersWaiters,
            ILogger logger)
        {
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _executionContextManager = executionContextManager;
            _eventHandlersWaiters = eventHandlersWaiters;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ICommandContext Build(CommandRequest command)
        {
            return new CommandContext(
                command,
                _executionContextManager.Current,
                _uncommittedEventStreamCoordinator,
                _eventHandlersWaiters,
                _logger);
        }
    }
}