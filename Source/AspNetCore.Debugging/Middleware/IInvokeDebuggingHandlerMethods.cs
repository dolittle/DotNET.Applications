// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.AspNetCore.Debugging.Handlers;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Represents a system that can invoke <see cref="IDebuggingHandler"/> methods.
    /// </summary>
    public interface IInvokeDebuggingHandlerMethods
    {
        /// <summary>
        /// Invoke the handle method of the <see cref="IDebuggingHandler"/> corresponding to the <see cref="HttpRequest"/> method with an artifact.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> to handle.</param>
        /// <param name="handler">The <see cref="IDebuggingHandler"/> to invoke the handle method on.</param>
        /// <param name="artifactType">The <see cref="Type"/> of the artifact to invoke the method with.</param>
        /// <param name="artifact">The artifact to invoke the method with.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task InvokeDebugginHandlerMethod(HttpContext context, IDebuggingHandler handler, Type artifactType, object artifact);
    }
}