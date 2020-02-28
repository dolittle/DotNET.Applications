// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlerMethod"/>.
    /// </summary>
    public class EventHandlerMethod : IEventHandlerMethod
    {
        readonly MethodInfo _methodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethod"/> class.
        /// </summary>
        /// <param name="eventType">The type of event the <see cref="EventHandlerMethod"/> is for.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> for the handle method.</param>
        public EventHandlerMethod(Type eventType, MethodInfo methodInfo)
        {
            EventType = eventType;
            _methodInfo = methodInfo;

            ThrowIfEventHandlerMethodIsNotAsynchronous(methodInfo);
        }

        /// <inheritdoc/>
        public Type EventType { get; }

        /// <inheritdoc/>
        public async Task Invoke(ICanHandleEvents handler, CommittedEvent @event)
        {
            ThrowIfInvalidHandleSignature(_methodInfo);
            try
            {
                var result = _methodInfo.Invoke(handler, new object[] { @event.Event, @event.DeriveContext() }) as Task;
                await result.ConfigureAwait(false);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        void ThrowIfInvalidHandleSignature(MethodInfo methodInfo)
        {
            if (methodInfo.Name != EventHandlers.HandleMethodName) throw new EventHandlerMethodHasInvalidMethodName(methodInfo, methodInfo.Name);
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != 2) throw new EventHandlerMethodMustTakeTwoParameters(methodInfo);
            if (!typeof(IEvent).IsAssignableFrom(parameters[0].ParameterType)) throw new EventHandlerMethodFirstParameterMustBeAnEvent(methodInfo, parameters[0].ParameterType);
            if (parameters[1].ParameterType != typeof(EventContext)) throw new EventHandlerMethodSecondParameterMustBeEventContext(methodInfo, parameters[1].ParameterType);
        }

        void ThrowIfEventHandlerMethodIsNotAsynchronous(MethodInfo methodInfo)
        {
            if (!typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
            {
                throw new EventHandlerMethodMustBeAsynchronous(methodInfo, EventType);
            }
        }
    }
}