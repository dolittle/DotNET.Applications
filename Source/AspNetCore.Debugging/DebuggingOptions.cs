// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging
{
    /// <summary>
    /// Options for the Dolittle Swagger debugging tools.
    /// </summary>
    public class DebuggingOptions
    {
        /// <summary>
        /// Gets or sets the base path of the debugging APIs.
        /// </summary>
        public PathString BasePath { get; set; } = "/api/Dolittle/Debugging";
    }
}