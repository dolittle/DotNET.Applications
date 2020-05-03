// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Events.Handling.Internal;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// An implementation of <see cref="IEventHandlerManager"/>.
    /// </summary>
    public class EventHandlerManager : IEventHandlerManager
    {
        const string HandleMethodName = "Handle";
        readonly IContainer _container;
        readonly IAsyncPolicyFor<EventHandlerManager> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerManager"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> that will be used to create instances of <see cref="EventHandlerProcessor{TEventType}"/>.</param>
        /// <param name="policy">The <see cref="IAsyncPolicyFor{T}"/> that defines reconnect policies for event handlers.</param>
        /// <param name="logger">The <see cref="ILogger"/> used for logging.</param>
        public EventHandlerManager(
            IContainer container,
            IAsyncPolicyFor<EventHandlerManager> policy,
            ILogger logger)
        {
            _container = container;
            _policy = policy;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task Register<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, ICanHandle<TEventType> handler, CancellationToken cancellationToken)
            where TEventType : IEvent
        {
            var handlerMethods = GetHandleMethodsFrom(handler);
            var processor = _container.Get<EventHandlerProcessor<TEventType>>();
            return Task.Run(() => Start(id, scope, partitioned, processor, handlerMethods, cancellationToken), cancellationToken);
        }

        Task Start<TEventType>(EventHandlerId id, ScopeId scope, bool partitioned, EventHandlerProcessor<TEventType> processor, IDictionary<Type, HandleMethod<TEventType>> handlers, CancellationToken cancellationToken)
            where TEventType : IEvent
            => _policy.Execute(
                async (cancellationToken) =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var receivedResponse = await processor.Register(id, scope, handlers, partitioned, cancellationToken).ConfigureAwait(false);
                        ThrowIfNotReceivedResponse(id, receivedResponse);
                        ThrowIfRegisterFailure(id, processor.RegisterFailure);
                        await processor.Handle(cancellationToken).ConfigureAwait(false);
                    }
                },
                cancellationToken);

        IDictionary<Type, HandleMethod<TEventType>> GetHandleMethodsFrom<TEventType>(ICanHandle<TEventType> handler)
            where TEventType : IEvent
        {
            var methods = new Dictionary<Type, HandleMethod<TEventType>>();
            var handlerType = handler.GetType();

            foreach (var method in handlerType.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                if (MethodHasHandleSignatureFor<IEvent>(method) && method.Name != HandleMethodName)
                {
                    throw new EventHandlerMethodWithCorrectSignatureButWrongName(method, HandleMethodName);
                }

                if (method.Name == HandleMethodName)
                {
                    if (MethodHasHandleSignatureFor<TEventType>(method) && TryGetFirstMethodParameter<TEventType>(method, out var type))
                    {
                        methods[type] = (HandleMethod<TEventType>)method.CreateDelegate(typeof(HandleMethod<TEventType>), handler);
                    }
                    else if (!FirstMethodParameterIs<TEventType>(method))
                    {
                        throw new EventHandlerMethodFirstParameterMustBe<TEventType>(method);
                    }
                    else if (!SecondMethodParameterIsEventContext(method))
                    {
                        throw new EventHandlerMethodSecondParameterMustBeEventContext(method);
                    }
                    else if (!MethodHasNoExtraParameters(method))
                    {
                        throw new EventHandlerMethodMustTakeTwoParameters(method);
                    }
                    else
                    {
                        throw new EventHandlerMethodMustReturnATask(method);
                    }
                }
            }

            return methods;
        }

        bool MethodHasHandleSignatureFor<TEventType>(MethodInfo method)
            where TEventType : IEvent
            => FirstMethodParameterIs<TEventType>(method) && SecondMethodParameterIsEventContext(method) && MethodHasNoExtraParameters(method) && MethodReturnsTask(method);

        bool FirstMethodParameterIs<TEventType>(MethodInfo method)
            where TEventType : IEvent
            => TryGetFirstMethodParameter<TEventType>(method, out _);

        bool TryGetFirstMethodParameter<TEventType>(MethodInfo method, out Type type)
            where TEventType : IEvent
        {
            type = null;
            if (method.GetParameters().Length > 0)
            {
                type = method.GetParameters()[0].ParameterType;
                return typeof(TEventType).IsAssignableFrom(type);
            }

            return false;
        }

        bool SecondMethodParameterIsEventContext(MethodInfo method)
            => method.GetParameters().Length > 1 && method.GetParameters()[1].ParameterType == typeof(EventContext);

        bool MethodHasNoExtraParameters(MethodInfo method)
            => method.GetParameters().Length == 2;

        bool MethodReturnsTask(MethodInfo method)
            => method.ReturnType == typeof(Task);

        void ThrowIfNotReceivedResponse(EventHandlerId id, bool receivedResponse)
        {
            if (!receivedResponse) throw new DidNotReceiveEventHandlerRegistrationResponse(id);
        }

        void ThrowIfRegisterFailure(EventHandlerId id, Failure registerFailure)
        {
            if (registerFailure != null) throw new EventHandlerRegistrationFailed(id, registerFailure);
        }
    }
}
