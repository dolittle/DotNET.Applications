// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that contains all the information needed to create a proxy file.
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// Gets or sets the EcmaScript (Javascript) content of the proxy.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the full filepath of the proxy file.
        /// </summary>
        public string FullFilePath { get; set; }
    }
}