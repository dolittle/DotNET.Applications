// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;

namespace Dolittle.Commands.Coordination
{
    /// <summary>
    /// Represents a <see cref="ICommandContextManager">Command context manager</see>.
    /// </summary>
    public class CommandContextManager : ICommandContextManager
    {
        static readonly AsyncLocal<ICommandContext> _currentContext = new AsyncLocal<ICommandContext>();

        readonly ICommandContextFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextManager"/> class.
        /// </summary>
        /// <param name="factory">A <see cref="ICommandContextFactory"/> to use for building an <see cref="ICommandContext"/>.</param>
        public CommandContextManager(ICommandContextFactory factory)
        {
            _factory = factory;
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
            if (!IsInContext(command))
            {
                _currentContext.Value = _factory.Build(command);
            }

            return _currentContext.Value;
        }

        static bool IsInContext(CommandRequest command) => _currentContext.Value?.Command.Equals(command) == true;
    }
}