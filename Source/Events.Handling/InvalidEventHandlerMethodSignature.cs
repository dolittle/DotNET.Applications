// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Exception that gets thrown when an event handler method has an invalid signature.
    /// </summary>
    public abstract class InvalidEventHandlerMethodSignature : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEventHandlerMethodSignature"/> class.
        /// </summary>
        /// <param name="methodInfo">The <see cerf="MethodInfo" /> of the handle method.</param>
        /// <param name="reason">The reason why it is invalid.</param>
        protected InvalidEventHandlerMethodSignature(MethodInfo methodInfo, string reason)
            : base($"The Event Handler method signature {methodInfo.Name}({string.Join(',', methodInfo.GetParameters().Select(_ => _.ParameterType.FullName))}) in class {methodInfo.DeclaringType.FullName} is invalid because: {reason}")
        {
        }
    }
}