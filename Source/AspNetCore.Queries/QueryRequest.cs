// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.AspNetCore.Queries
{
    /// <summary>
    /// Represents the request for performing a query.
    /// </summary>
    public class QueryRequest
    {
        /// <summary>
        /// Gets or sets the name of the query.
        /// </summary>
        public string NameOfQuery { get; set; }

        /// <summary>
        /// Gets or sets the assembly qualified string representing the type of the query.
        /// </summary>
        public string GeneratedFrom { get; set; }

        /// <summary>
        /// Gets or sets all the parameters matching any properties on the query with their values.
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}