// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.AspNetCore.Debugging.Handlers;
using Microsoft.OpenApi.Models;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// Represents a system that can generate the <see cref="OpenApiDocument"/> for an <see cref="IDebuggingHandler"/>.
    /// </summary>
    public interface IDebuggingHandlerDocumentGenerator
    {
        /// <summary>
        /// Generates an <see cref="OpenApiDocument"/> for an <see cref="IDebuggingHandler"/>.
        /// </summary>
        /// <param name="handler">The debugging handler to generate a document for.</param>
        /// <returns>The api document describing the operations of the debugging handler.</returns>
        OpenApiDocument GenerateFor(IDebuggingHandler handler);
    }
}