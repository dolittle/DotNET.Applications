/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a wrapper for the <see cref="Applications.Configuration.Topology"/> with the other information that is needed to generate the topology
    /// </summary>
    public class BoundedContextTopology
    {
        /// <summary>
        /// The <see cref="Applications.Configuration.Topology"/>
        /// </summary>
        /// <value></value>
        public Applications.Configuration.Topology Topology {get; set;}
        
        /// <summary>
        /// Whether or not the Topology should build using Modules or not
        /// </summary>
        public bool UseModules {get; }

        /// <summary>
        /// A mapping from <see cref="Area"/> to a string representing a segment in the namespace that the user wishes to exclude from the Module/Feature
        /// </summary>
        public IDictionary<Area, IEnumerable<string>> NamespaceSegmentsToStrip {get; }

        /// <summary>
        /// Instantiates an instance of <see cref="BoundedContextTopology"/>
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="useModules"></param>
        /// <param name="namespaceSegmentsToStrip"></param>
        public BoundedContextTopology(Applications.Configuration.Topology topology, bool useModules, IDictionary<Area, IEnumerable<string>> namespaceSegmentsToStrip)
        {
            Topology = topology;
            UseModules = useModules;
            NamespaceSegmentsToStrip = namespaceSegmentsToStrip;
        }

    }
}