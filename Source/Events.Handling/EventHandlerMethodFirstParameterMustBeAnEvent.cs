// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an Event Handler handle method signature's first parameter is not an <see cref="IEvent" />.
    /// </summary>
    public class EventHandlerMethodFirstParameterMustBeAnEvent : InvalidEventHandlerMethodSignature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodFirstParameterMustBeAnEvent"/> class.
        /// </summary>
        /// <param name="methodInfo">The Event Handler Handle <see cref="MethodInfo" />.</param>
        /// <param name="parameterType">The <see cref="Type" /> of the first parameter in the event handler method.</param>
        /// <param name="expectedEventParentType">The expected event parent <see cref="Type" />.</param>
        public EventHandlerMethodFirstParameterMustBeAnEvent(MethodInfo methodInfo, Type parameterType, Type expectedEventParentType)
            : base(methodInfo, $"The Event Handler '{AbstractEventHandler.HandleMethodName}' method's first parameter must inherit from {expectedEventParentType.FullName}. Got parameter type: {parameterType.FullName}")
        {
        }
    }
}