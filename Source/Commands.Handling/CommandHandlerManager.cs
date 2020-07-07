// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Represents a <see cref="ICommandHandlerManager">ICommandHandlerManager</see>.
    /// </summary>
    /// <remarks>
    /// The manager will automatically import any <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see>
    /// and use them when handling.
    /// </remarks>
    [Singleton]
    public class CommandHandlerManager : ICommandHandlerManager
    {
        readonly IEnumerable<ICommandHandlerInvoker> _invokers;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerManager"/> class.
        /// </summary>
        /// <param name="invokers">
        /// <see cref="IInstancesOf{ICommandHandlerInvoker}">Invokers</see> to use for discovering the.
        /// <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see>'s to use.
        /// </param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public CommandHandlerManager(IInstancesOf<ICommandHandlerInvoker> invokers, ILogger logger)
        {
            _invokers = invokers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Handle(CommandRequest command)
        {
            var handled = false;
            _logger.Trace("Invoking command handlers for command {CommandType} with correlation {Correlation}", command.Type, command.CorrelationId);
            foreach (var invoker in _invokers)
            {
                if (invoker.TryHandle(command))
                {
                    handled = true;
                }
            }

            ThrowIfNotHandled(command, handled);
        }

        void ThrowIfNotHandled(CommandRequest command, bool handled)
        {
            if (!handled) throw new CommandWasNotHandled(command);
        }
    }
}