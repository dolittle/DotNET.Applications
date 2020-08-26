// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.AspNetCore.Debugging.Handlers;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Exception that gets thrown when no appropriate Handle... method is found on an <see cref="IDebuggingHandler"/> using a handler interface for an artifact type.
    /// </summary>
    public class NoAppropriateHandleMethodFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoAppropriateHandleMethodFound"/> class.
        /// </summary>
        /// <param name="handler">The <see cref="IDebuggingHandler"/> that was examined.</param>
        /// <param name="handlerInterface">The <see cref="Type"/> of the handler interface that was used.</param>
        /// <param name="artifactType">The <see cref="Type"/> of the artifact that should be handled.</param>
        public NoAppropriateHandleMethodFound(IDebuggingHandler handler, Type handlerInterface, Type artifactType)
            : base($"No appropriate Handle... method was found for {handler.GetType()} using the {handlerInterface} interface for artifacts of type {artifactType}.")
        {
        }
    }
}