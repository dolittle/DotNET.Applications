// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// The Query proxy that's fed into the Handlebars templating engine.
    /// </summary>
    public class HandlebarsQuery
    {
        /// <summary>
        /// Gets or sets the name of the Query.
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the Clr type which the query proxy is generated from.
        /// </summary>
        public string ClrType { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ProxyProperty"/>that represents the Properties or Arguments of the proxy.
        /// </summary>
        public IEnumerable<ProxyProperty> Properties { get; set; } = new List<ProxyProperty>();
    }
}