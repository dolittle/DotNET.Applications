// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="MethodInfo"/> on an <see cref="EventHandler"/> is asynchronous and returns void.
    /// </summary>
    public class EventHandlerMethodCannotBeAsyncVoid : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodCannotBeAsyncVoid"/> class.
        /// </summary>
        /// <param name="methodInfo"><see cref="MethodInfo"/> that is violating.</param>
        /// <param name="eventType"><see cref="Type"/> of event the handler represents.</param>
        public EventHandlerMethodCannotBeAsyncVoid(MethodInfo methodInfo, Type eventType)
            : base($"Handle method for event '{eventType.AssemblyQualifiedName}' on handler '{methodInfo.DeclaringType.AssemblyQualifiedName}' is async void. This is not allowed, it should instead return Task.")
        {
        }
    }
}