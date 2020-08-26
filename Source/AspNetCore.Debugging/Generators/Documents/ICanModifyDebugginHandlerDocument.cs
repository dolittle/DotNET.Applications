// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.AspNetCore.Debugging.Handlers;
using Microsoft.OpenApi.Models;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// Represents a system that can modify the <see cref="OpenApiDocument"/> generated for a <see cref="IDebuggingHandler"/>.
    /// </summary>
    public interface ICanModifyDebugginHandlerDocument
    {
        /// <summary>
        /// Modifies the generated document for a debugging handler.
        /// </summary>
        /// <param name="handler">The <see cref="IDebuggingHandler"/> the document was generated for.</param>
        /// <param name="document">The <see cref="OpenApiDocument"/> to modify.</param>
        void ModifyDocument(IDebuggingHandler handler, OpenApiDocument document);
    }
}