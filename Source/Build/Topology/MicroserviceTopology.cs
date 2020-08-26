// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a wrapper for the <see cref="Applications.Configuration.Topology"/> with the other information that is needed to generate the topology.
    /// </summary>
    public class MicroserviceTopology
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicroserviceTopology"/> class.
        /// </summary>
        /// <param name="topology"><see cref="Applications.Configuration.Topology"/> configuration.</param>
        /// <param name="useModules">Whether or not to use modules.</param>
        /// <param name="namespaceSegmentsToStrip">Namespace segments to strip.</param>
        public MicroserviceTopology(Applications.Configuration.Topology topology, bool useModules, IDictionary<Area, IEnumerable<string>> namespaceSegmentsToStrip)
        {
            Topology = topology;
            UseModules = useModules;
            NamespaceSegmentsToStrip = namespaceSegmentsToStrip;
        }

        /// <summary>
        /// Gets or sets the <see cref="Applications.Configuration.Topology"/>.
        /// </summary>
        public Applications.Configuration.Topology Topology { get; set; }

        /// <summary>
        /// Gets a value indicating whether whether or not the Topology should build using Modules or not.
        /// </summary>
        public bool UseModules { get; }

        /// <summary>
        /// Gets a mapping from <see cref="Area"/> to a string representing a segment in the namespace that the user wishes to exclude from the Module/Feature.
        /// </summary>
        public IDictionary<Area, IEnumerable<string>> NamespaceSegmentsToStrip { get; }
    }
}