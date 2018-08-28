/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="BoundedContext"/> for configuration
    /// </summary>
    public class BoundedContextConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="Application"/>
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BoundedContext"/>
        /// </summary>
        public BoundedContext BoundedContext { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BoundedContextName"/> 
        /// </summary>
        public BoundedContextName BoundedContextName {get; set;}

        /// <summary>
        /// Gets or sets whether or not one is using <see cref="Module">modules</see>
        /// </summary>
        public bool UseModules { get; set; }

        /// <summary>
        /// Gets or sets whether or not one wants to generate proxies
        /// </summary>
        public bool GenerateProxies {get; set;}
        
        /// <summary>
        /// Gets or sets the base path for proxies
        /// </summary>
        public string ProxiesBasePath {get; set;}

        /// <summary>
        /// Gets or sets a mapping from <see cref="Area"/> to a string representing a segment in the namespace that the user wishes to exclude from the Module/Feature
        /// </summary>
        public Dictionary<Area, IEnumerable<string>> NamespaceSegmentsToStrip {get; set;} = new Dictionary<Area, IEnumerable<string>>();

        /// <summary>
        /// Gets or sets the <see cref="TopologyConfiguration"/> for the <see cref="BoundedContext"/>
        /// </summary>
        public TopologyConfiguration Topology { get; set; }
    }
}