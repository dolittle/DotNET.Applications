// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            ThrowIfEventHandlerMethodIsAsyncVoid(methodInfo);
        }

        /// <inheritdoc/>
        public Type EventType { get; }

        /// <inheritdoc/>
        public async Task Invoke(ICanHandleEvents handler, CommittedEvent @event)
        {
            var result = _methodInfo.Invoke(handler, new[] { @event.Event }) as Task;
            await result.ConfigureAwait(false);
        }

        void ThrowIfEventHandlerMethodIsNotAsynchronous(MethodInfo methodInfo)
        {
            if (!typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
            {
                throw new EventHandlerMethodMustBeAsynchronous(methodInfo, EventType);
            }
        }

        void ThrowIfEventHandlerMethodIsAsyncVoid(MethodInfo methodInfo)
        {
            var asyncStateMachineAttribute = (AsyncStateMachineAttribute)methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute));

            if (asyncStateMachineAttribute == null && methodInfo.ReturnType == typeof(void))
            {
                throw new EventHandlerMethodCannotBeAsyncVoid(methodInfo, EventType);
            }
        }
    }
}