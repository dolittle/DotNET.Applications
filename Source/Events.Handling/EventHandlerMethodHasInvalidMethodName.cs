// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an Event Handler handle method signature's method name is invalid.
    /// </summary>
    public class EventHandlerMethodHasInvalidMethodName : InvalidEventHandlerMethodSignature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodHasInvalidMethodName"/> class.
        /// </summary>
        /// <param name="methodInfo">The Event Handler Handle <see cref="MethodInfo" />.</param>
        /// <param name="methodName">The name of the method.</param>
        public EventHandlerMethodHasInvalidMethodName(MethodInfo methodInfo, string methodName)
            : base(methodInfo, $"The Event Handler method's name must be '{AbstractEventHandler.HandleMethodName}', but was '{methodName}'")
        {
        }
    }
}