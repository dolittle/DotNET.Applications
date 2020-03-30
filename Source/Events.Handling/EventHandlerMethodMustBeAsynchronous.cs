// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="MethodInfo"/> on an event handler is not asynchronous.
    /// </summary>
    public class EventHandlerMethodMustBeAsynchronous : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodMustBeAsynchronous"/> class.
        /// </summary>
        /// <param name="methodInfo"><see cref="MethodInfo"/> that is violating.</param>
        /// <param name="eventType"><see cref="Type"/> of event the handler represents.</param>
        public EventHandlerMethodMustBeAsynchronous(MethodInfo methodInfo, Type eventType)
            : base($"{AbstractEventHandler.HandleMethodName} method for event '{eventType.AssemblyQualifiedName}' on event handler handler '{methodInfo.DeclaringType.AssemblyQualifiedName}' is not asynchronous. Remember to return a Task that can be awaited")
        {
        }
    }
}