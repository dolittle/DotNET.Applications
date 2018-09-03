/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Artifacts;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// The Query proxy that's fed into the Handlebars templating engine
    /// </summary>
    public class HandlebarsQuery
    {
        /// <summary>
        /// Gets and sets the name of the Query
        /// </summary>
        /// <value></value>
        public string QueryName {get; set;}
        /// <summary>
        /// Gets and sets the string representation of the Clr type which the query proxy is generated from 
        /// </summary>
        /// <value></value>
        public string ClrType {get; set;}
        /// <summary>
        /// Gets and sets the list of <see cref="ProxyProperty"/>that represents the Properties or Arguments of the proxy
        /// </summary>
        public IEnumerable<ProxyProperty> Properties { get; set ;} = new List<ProxyProperty>();
    }
}