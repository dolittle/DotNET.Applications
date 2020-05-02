// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an Event Handler handle method has the correct signature but the wrong name.
    /// </summary>
    public class EventHandlerMethodWithCorrectSignatureButWrongName : InvalidEventHandlerMethodSignature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethodWithCorrectSignatureButWrongName"/> class.
        /// </summary>
        /// <param name="methodInfo">The Event Handler Handle <see cref="MethodInfo" />.</param>
        /// <param name="methodName">The expected method name.</param>
        public EventHandlerMethodWithCorrectSignatureButWrongName(MethodInfo methodInfo, string methodName)
            : base(methodInfo, $"has the correct {methodName} method signature, but another name. If it is not supposed to be a {methodName} make the method non-public.")
        {
        }
    }
}