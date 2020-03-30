// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that can provide <see cref="AbstractEventHandlerForEventHandlerType{TEventHandler}" /> for <see cref="ICanHandleEvents" />.
    /// </summary>
    public class EventHandlerProvider : ICanProvideEventHandlers
    {
        readonly IImplementationsOf<ICanHandleEvents> _eventHandlerTypes;
        readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProvider"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes">The <see cref="IImplementationsOf{T}" /> of <see cref="ICanHandleEvents" />.</param>
        /// <param name="container">The <see cref="IContainer" />.</param>
        public EventHandlerProvider(IImplementationsOf<ICanHandleEvents> eventHandlerTypes, IContainer container)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractEventHandler> Provide() => _eventHandlerTypes.Select(type =>
            {
                ThrowIfMissingAttributeForEventHandler(type);
                var eventHandlerId = type.GetCustomAttribute<EventHandlerAttribute>().Id;
                var eventMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName && TakesExpectedParameters(_));

                var eventHandlerMethods = eventMethods.Select(_ => new EventHandlerMethod<IEvent>(_.GetParameters()[0].ParameterType, _));
                return new EventHandler(_container, eventHandlerId, type, IsPartitioned(type), eventHandlerMethods);
            });

        bool IsPartitioned(Type type) =>
            !type.HasAttribute<NotPartitionedAttribute>();

        bool TakesExpectedParameters(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            return parameters.Length == 2 &&
                    typeof(IEvent).IsAssignableFrom(parameters[0].ParameterType) &&
                    parameters[1].ParameterType == typeof(EventContext);
        }

        void ThrowIfMissingAttributeForEventHandler(Type type)
        {
            if (!type.HasAttribute<EventHandlerAttribute>())
            {
                throw new MissingEventHandlerAttributeForEventHandler(type);
            }
        }
    }
}