// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Booting;
using Dolittle.Events.Handling.EventHorizon;
using Dolittle.Events.Handling.Internal;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an <see cref="ICanPerformBootProcedure"/> that registers event handlers with the Runtime.
    /// </summary>
    public class RegistrationBootProcedure : ICanPerformBootProcedure
    {
        readonly IEventHandlerManager _manager;
        readonly IInstancesOf<ICanProvideEventHandlers> _handlerProviders;
        readonly IInstancesOf<ICanProvideExternalEventHandlers> _externalHandlerProviders;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationBootProcedure"/> class.
        /// </summary>
        /// <param name="manager">The <see cref="IEventHandlerManager"/> that will be used to register the event handlers.</param>
        /// <param name="handlerProviders">Providers of <see cref="ICanHandleEvents"/>.</param>
        /// <param name="externalHandlerProviders">Providers of <see cref="ICanHandleExternalEvents"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public RegistrationBootProcedure(
            IEventHandlerManager manager,
            IInstancesOf<ICanProvideEventHandlers> handlerProviders,
            IInstancesOf<ICanProvideExternalEventHandlers> externalHandlerProviders,
            ILogger logger)
        {
            _manager = manager;
            _handlerProviders = handlerProviders;
            _externalHandlerProviders = externalHandlerProviders;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Debug("Discovering event handlers in boot procedure");
            foreach (var provider in _handlerProviders) RegisterHandlersFromProvider(provider);
            foreach (var provider in _externalHandlerProviders) RegisterHandlersFromProvider(provider);
        }

        void RegisterHandlersFromProvider<THandlerType, TEventType>(ICanProvideHandlers<THandlerType, TEventType> provider)
            where THandlerType : class, ICanHandle<TEventType>
            where TEventType : IEvent
        {
            var type = provider.GetType();
            _logger.Trace("Registering event handlers from {HandlerProvider}", type);
            try
            {
                var registerHandlerMethod = GetType().GetMethod(nameof(RegisterHandler), BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var handler in provider.Provide())
                {
                    registerHandlerMethod.MakeGenericMethod(handler, typeof(TEventType)).Invoke(this, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Error while providing event handlers from {HandlerProvider}", type);
            }
        }

        void RegisterHandler<THandlerType, TEventType>()
            where THandlerType : class, ICanHandle<TEventType>
            where TEventType : IEvent
        {
            var type = typeof(THandlerType);
            _logger.Trace("Registering event handler {Handler}", type);

            if (!type.HasAttribute<EventHandlerAttribute>())
            {
                _logger.Warning("Event handler {Handler} is missing the required [EventHandler(...)] attribute. It will not be registered.");
                return;
            }

            var handlerId = type.GetCustomAttribute<EventHandlerAttribute>().Id;
            var scopeId = type.HasAttribute<ScopeAttribute>() ? type.GetCustomAttribute<ScopeAttribute>().Id : ScopeId.Default;
            var partitioned = !type.HasAttribute<NotPartitionedAttribute>();

            Task.Run(async () =>
            {
                try
                {
                    await _manager.Register<THandlerType, TEventType>(handlerId, scopeId, partitioned).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex, "Error while registering event handler {Handler}.", type);
                }
            });
        }
    }
}