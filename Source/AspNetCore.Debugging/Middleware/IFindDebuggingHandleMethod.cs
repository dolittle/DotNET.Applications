// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.AspNetCore.Debugging.Handlers;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Represents a system that can find the Handle... method of a <see cref="IDebuggingHandler"/>.
    /// </summary>
    public interface IFindDebuggingHandleMethod
    {
        /// <summary>
        /// Finds the appropriate Handle... method of a <see cref="IDebuggingHandler"/> given a handler interface and an artifact type.
        /// </summary>
        /// <param name="handler">The <see cref="IDebuggingHandler"/> that implements the handler interface.</param>
        /// <param name="handlerInterface">The <see cref="Type"/> of the handler interface.</param>
        /// <param name="artifactType">The <see cref="Type"/> of the artifact that will be used to call the Handle... method.</param>
        /// <returns>The <see cref="MethodInfo"/> of the appropriate Handle... method.</returns>
        MethodInfo FindMethod(IDebuggingHandler handler, Type handlerInterface, Type artifactType);
    }
}