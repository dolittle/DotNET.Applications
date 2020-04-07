// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Commands.Handling
{
    /// <summary>
    /// Represents a <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see> for handling
    /// command handlers that have methods called Handle() and takes specific <see cref="ICommand">commands</see>
    /// in as parameters.
    /// </summary>
    [Singleton]
    public class CommandHandlerInvoker : ICommandHandlerInvoker
    {
        const string HandleMethodName = "Handle";

        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        readonly IArtifactTypeMap _artifactTypeMap;
        readonly ICommandRequestToCommandConverter _converter;
        readonly ILogger _logger;
        readonly Dictionary<Artifact, MethodInfo> _commandHandlers = new Dictionary<Artifact, MethodInfo>();
        readonly object _initializationLock = new object();
        bool _initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerInvoker"/> class.
        /// </summary>
        /// <param name="typeFinder">A <see cref="ITypeFinder"/> to use for discovering <see cref="ICanHandleCommands">command handlers</see>.</param>
        /// <param name="container">A <see cref="IContainer"/> to use for getting instances of objects.</param>
        /// <param name="artifactTypeMap"><see cref="IArtifactTypeMap"/> for identifying resources.</param>
        /// <param name="converter"><see cref="ICommandRequestToCommandConverter"/> for converting to actual <see cref="ICommand"/> instances.</param>
        /// <param name="logger"><see cref="ILogger"/> used for logging.</param>
        public CommandHandlerInvoker(
            ITypeFinder typeFinder,
            IContainer container,
            IArtifactTypeMap artifactTypeMap,
            ICommandRequestToCommandConverter converter,
            ILogger logger)
        {
            _typeFinder = typeFinder;
            _container = container;
            _artifactTypeMap = artifactTypeMap;
            _converter = converter;
            _logger = logger;
            _initialized = false;
        }

        /// <summary>
        /// Register a command handler explicitly.
        /// </summary>
        /// <param name="handlerType"><see cref="Type"/> holding the handler.</param>
        /// <remarks>
        /// The registration process will look into the handler and find methods that
        /// are called Handle() and takes a command as parameter.
        /// </remarks>
        public void Register(Type handlerType)
        {
            var handleMethods = handlerType
                .GetRuntimeMethods()
                .Where(m => m.IsPublic || !m.IsStatic)
                .Where(m => m.Name.Equals(HandleMethodName, StringComparison.InvariantCulture))
                .Where(m => m.GetParameters().Length == 1)
                .Where(m => typeof(ICommand).GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType));

            handleMethods.ForEach(method =>
            {
                var commandType = method.GetParameters()[0].ParameterType;
                var identifier = _artifactTypeMap.GetArtifactFor(commandType);
                _commandHandlers[identifier] = method;
            });
        }

        /// <inheritdoc/>
        public bool TryHandle(CommandRequest command)
        {
            EnsureInitialized();

            _logger.Debug($"Trying to invoke command handlers for {command.Type}");

            if (_commandHandlers.Count == 0) return false;

            if (_commandHandlers.ContainsKey(command.Type))
            {
                var commandHandlerType = _commandHandlers[command.Type].DeclaringType;
                _logger.Trace($"Trying command handler '{commandHandlerType.AssemblyQualifiedName}'");
                var commandHandler = _container.Get(commandHandlerType);
                var method = _commandHandlers[command.Type];
                var commandInstance = _converter.Convert(command);

                _logger.Trace($"Invoke");
                try
                {
                    method.Invoke(commandHandler, new[] { commandInstance });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Failed invoking command handler '{commandHandlerType.AssemblyQualifiedName}' for command of type '{command.Type}'");
                    return false;
                }

                return true;
            }
            else
            {
                _logger.Debug("No command handlers to invoke");
            }

            return false;
        }

        void EnsureInitialized()
        {
            if (_initialized) return;

            lock (_initializationLock)
            {
                if (!_initialized) Initialize();
                _initialized = true;
            }
        }

        void Initialize()
        {
            var handlers = _typeFinder.FindMultiple<ICanHandleCommands>();
            handlers.ForEach(Register);
        }
    }
}
