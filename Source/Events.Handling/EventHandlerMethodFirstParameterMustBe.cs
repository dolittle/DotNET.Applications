// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an Event Handler handle method signature's first parameter is not an <typeparamref name="TEventType"/>.
    /// </summary>
    /// <typeparam name="TEventType">The event type that the filter can handle.</typeparam>
    public class EventHandlerMethodFirstParameterMustBe<TEventType> : InvalidEventHandlerMethodSignature
        where TEventType : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodFirstParameterMustBe{TEventType}"/> class.
        /// </summary>
        /// <param name="methodInfo">The Event Handler Handle <see cref="MethodInfo" />.</param>
        public EventHandlerMethodFirstParameterMustBe(MethodInfo methodInfo)
            : base(methodInfo, $"is invalid. The first parameter must implement {typeof(TEventType)}.")
        {
        }
    }
}