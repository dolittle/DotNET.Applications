// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Handlers
{
    /// <summary>
    /// Represents a system that handles Swagger debugging operations.
    /// </summary>
    public interface IDebuggingHandler
    {
        /// <summary>
        /// Gets the name of the debugging handler. Must be unique across handlers.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the title describing the debugging handler. Must be unique across handlers.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the <see cref="IDictionary{PathString, Type}"/> which maps paths to artifacts the handler can handle.
        /// </summary>
        IDictionary<PathString, Type> Artifacts { get; }

        /// <summary>
        /// Gets the <see cref="IDictionary{Int, String}"/> which describes possible responses from the handler.
        /// </summary>
        IDictionary<int, string> Responses { get; }
    }
}