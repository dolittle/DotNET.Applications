// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.AspNetCore.Debugging.Handlers;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Exception that gets thrown when invocation of a Handle... method on an <see cref="IDebuggingHandler"/> with an artifact fails.
    /// </summary>
    public class CouldNotInvokeHandleMethod : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouldNotInvokeHandleMethod"/> class.
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo"/> of the method that was invoked.</param>
        /// <param name="handler">The <see cref="IDebuggingHandler"/> that the method was invoked on.</param>
        /// <param name="artifactType">The <see cref="Type"/> of the artifact that was used to call the method.</param>
        /// <param name="exception">The actual <see cref="Exception"/> that was thrown.</param>
        public CouldNotInvokeHandleMethod(MethodInfo method, IDebuggingHandler handler, Type artifactType, Exception exception)
            : base($"Could not invoke method {method} on handler {handler} with artifact of type {artifactType}", exception)
        {
        }
    }
}