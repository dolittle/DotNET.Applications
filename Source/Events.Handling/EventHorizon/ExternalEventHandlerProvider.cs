// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Defines a system that can provide <see cref="AbstractEventHandlerForEventHandlerType{TEventHandler}" /> for <see cref="ICanHandleExternalEvents" />.
    /// </summary>
    public class ExternalEventHandlerProvider : ICanProvideEventHandlers
    {
        readonly IImplementationsOf<ICanHandleExternalEvents> _eventHandlerTypes;
        readonly IContainer _container;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProvider"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes">The <see cref="IImplementationsOf{T}" /> of <see cref="ICanHandleExternalEvents" />.</param>
        /// <param name="container">The <see cref="IContainer" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public ExternalEventHandlerProvider(
            IImplementationsOf<ICanHandleExternalEvents> eventHandlerTypes,
            IContainer container,
            ILogger<ExternalEventHandlerProvider> logger)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _container = container;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractEventHandler> Provide()
        {
            var eventHandlers = new List<AbstractEventHandler>();
            foreach (var eventHandlerType in _eventHandlerTypes)
            {
                if (!HasEventHandlerAttribute(eventHandlerType))
                {
                    WarnEventHandlerMissingEventHandlerAttribute(eventHandlerType);
                    continue;
                }

                if (!HasScopeAttribute(eventHandlerType))
                {
                    WarnEventHandlerMissingScopeAttribute(eventHandlerType);
                    continue;
                }

                var eventHandlerId = eventHandlerType.GetCustomAttribute<EventHandlerAttribute>().Id;
                var scopeId = eventHandlerType.GetCustomAttribute<ScopeAttribute>().Id;
                var handleMethods = eventHandlerType
                                    .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName);
                var hasInvalidHandlerMethod = false;
                foreach (var method in handleMethods)
                {
                    if (!FirstParameterIsPublicEvent(method))
                    {
                        WarnFirstParameterIsNotPublicEvent(method);
                        hasInvalidHandlerMethod = true;
                    }

                    if (!SecondParameterIsEventContext(method))
                    {
                        WarnSecondParameterIsNotEventContext(method);
                        hasInvalidHandlerMethod = true;
                    }

                    if (FirstParameterIsPublicEvent(method) && !HasArtifactAttribute(method.GetParameters()[0].ParameterType))
                    {
                        WarnEventMissingArtifactIdAttribute(method.GetParameters()[0].ParameterType);
                    }
                }

                if (hasInvalidHandlerMethod)
                {
                    _logger.Warning(
                        "Could not register External Event Handler '{eventHandlerName} : {eventHandlerInterfaceName}' because some of the Handle methods are invalid",
                        eventHandlerType.ToString(),
                        typeof(ICanHandleExternalEvents).ToString());
                    continue;
                }

                var eventHandlerMethods = handleMethods.Select(_ => new EventHandlerMethod<IPublicEvent>(_.GetParameters()[0].ParameterType, _));

                eventHandlers.Add(new ExternalEventHandler(_container, eventHandlerId, eventHandlerType, scopeId, eventHandlerMethods));
            }

            return eventHandlers;
        }

        bool HasEventHandlerAttribute(Type eventHandlerType) => eventHandlerType.HasAttribute<EventHandlerAttribute>();

        bool HasScopeAttribute(Type eventHandlerType) => eventHandlerType.HasAttribute<ScopeAttribute>();

        void WarnEventHandlerMissingEventHandlerAttribute(Type eventHandlerType) =>
            _logger.Warning(
                "Could not register External Event Handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{eventHandlerAttribute}' attribute",
                eventHandlerType.ToString(),
                typeof(ICanHandleExternalEvents).ToString(),
                typeof(EventHandlerAttribute).ToString());

        void WarnEventHandlerMissingScopeAttribute(Type eventHandlerType) =>
            _logger.Warning(
                    "Could not register External Event Handler '{eventHandlerName} : {externalEventHandlerInterfaceName}' because it is missing the '{scopeAttribute}' attribute",
                    eventHandlerType.ToString(),
                    typeof(ICanHandleExternalEvents).ToString(),
                    typeof(ScopeAttribute).ToString());

        bool FirstParameterIsPublicEvent(MethodInfo methodInfo) => typeof(IPublicEvent).IsAssignableFrom(methodInfo.GetParameters()[0]?.ParameterType);

        bool SecondParameterIsEventContext(MethodInfo methodInfo) => methodInfo.GetParameters()[1]?.ParameterType == typeof(EventContext);

        void WarnFirstParameterIsNotPublicEvent(MethodInfo methodInfo) =>
            _logger.Warning(
                "Could not register the External Event Handler Method: {eventHandlerMethod} in event handler '{eventHandler}'. The first parameter has to be an event that implements: '{publicEvent}'",
                $"{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(_ => _.ParameterType.ToString()))})",
                methodInfo.DeclaringType.ToString(),
                typeof(IPublicEvent).ToString());

        void WarnSecondParameterIsNotEventContext(MethodInfo methodInfo) =>
            _logger.Warning(
                "Could not register the External Event Handler Method: {eventHandlerMethod} in event handler '{eventHandler}'. The second parameter has to be the context of the event: '{eventContext}'",
                $"{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(_ => _.ParameterType.ToString()))})",
                methodInfo.DeclaringType.ToString(),
                typeof(EventContext).ToString());

        bool HasArtifactAttribute(Type eventType) => eventType.HasAttribute<ArtifactAttribute>();

        void WarnEventMissingArtifactIdAttribute(Type eventType) =>
            _logger.Warning(
                "Cannot have an External Event Handler on event '{event}' because it does not have an '{artifactAttribute}' attribute. External '{publicEvent}' must have an '{artifactAttribute}' attribute on the class",
                eventType.ToString(),
                typeof(ArtifactAttribute).ToString(),
                typeof(IPublicEvent).ToString(),
                typeof(ArtifactAttribute).ToString());
    }
}