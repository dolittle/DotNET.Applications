// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Events.EventHorizon;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventHandlerProvider"/> class.
        /// </summary>
        /// <param name="eventHandlerTypes">The <see cref="IImplementationsOf{T}" /> of <see cref="ICanHandleExternalEvents" />.</param>
        /// <param name="container">The <see cref="IContainer" />.</param>
        public ExternalEventHandlerProvider(IImplementationsOf<ICanHandleExternalEvents> eventHandlerTypes, IContainer container)
        {
            _eventHandlerTypes = eventHandlerTypes;
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractEventHandler> Provide() => _eventHandlerTypes.Select(eventHandlerType =>
            {
                ThrowIfMissingAttributes(eventHandlerType);
                var eventHandlerId = eventHandlerType.GetCustomAttribute<EventHandlerAttribute>().Id;
                var scopeId = eventHandlerType.GetCustomAttribute<ScopeAttribute>().Id;
                var eventMethods = eventHandlerType.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                                    .Where(_ => _.Name == AbstractEventHandler.HandleMethodName && TakesExpectedParameters(_));
                var eventTypesAndMethods = eventMethods.Select(_ =>
                {
                    (Type eventType, MethodInfo methodInfo) = (_.GetParameters()[0].ParameterType, _);
                    return (eventType, methodInfo);
                });
                var producerMicroservices = new List<Microservice>();
                var eventHandlerMethods = new List<EventHandlerMethod<IExternalEvent>>();
                eventTypesAndMethods.ForEach(eventTypeAndMethod =>
                {
                    (var eventType, var methodInfo) = (eventTypeAndMethod.eventType, eventTypeAndMethod.methodInfo);
                    ThrowIfMissingProducerMicroservice(eventType);
                    var producerMicroservice = eventType.GetCustomAttribute<ProducerMicroserviceAttribute>().Id;
                    ThrowIfIllegalProducerMicroserviceId(eventType, producerMicroservice);
                    producerMicroservices.Add(producerMicroservice);
                    eventHandlerMethods.Add(new EventHandlerMethod<IExternalEvent>(eventType, methodInfo));
                });

                return new ExternalEventHandler(_container, eventHandlerId, eventHandlerType, scopeId, producerMicroservices, eventHandlerMethods);
            });

        void ThrowIfIllegalProducerMicroserviceId(Type eventType, Microservice producerMicroservice)
        {
            ThrowIfProducerMicroserviceIdIsNotSet(eventType, producerMicroservice);
        }

        void ThrowIfProducerMicroserviceIdIsNotSet(Type eventType, Microservice producerMicroservice)
        {
            if (producerMicroservice == Microservice.NotSet)
            {
                throw new ProducerMicroserviceIdMustBeSet(eventType);
            }
        }

        bool TakesExpectedParameters(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            return parameters.Length == 2 &&
                    typeof(IExternalEvent).IsAssignableFrom(parameters[0].ParameterType) &&
                    parameters[1].ParameterType == typeof(EventContext);
        }

        void ThrowIfMissingAttributes(Type eventHandlerType)
        {
            ThrowIfMissingEventHandlerAttribute(eventHandlerType);
            ThrowIfMissingScopeAttribute(eventHandlerType);
        }

        void ThrowIfMissingEventHandlerAttribute(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<EventHandlerAttribute>())
            {
                throw new MissingEventHandlerAttributeForEventHandler(eventHandlerType);
            }
        }

        void ThrowIfMissingScopeAttribute(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<ScopeAttribute>())
            {
                throw new MissingScopeAttributeForExternalEventHandler(eventHandlerType);
            }
        }

        void ThrowIfMissingProducerMicroservice(Type eventHandlerType)
        {
            if (!eventHandlerType.HasAttribute<ProducerMicroserviceAttribute>())
            {
                throw new MissingScopeAttributeForExternalEventHandler(eventHandlerType);
            }
        }
    }
}