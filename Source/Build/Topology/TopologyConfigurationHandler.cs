// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a class that handles the interations with a <see cref="BoundedContextConfiguration"/>.
    /// </summary>
    public class TopologyConfigurationHandler
    {
        readonly ITopologyConfigurationManager _configurationManager;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopologyConfigurationHandler"/> class.
        /// </summary>
        /// <param name="configurationManager">The <see cref="ITopologyConfigurationManager"/>.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages.</param>
        public TopologyConfigurationHandler(ITopologyConfigurationManager configurationManager, IBuildMessages buildMessages)
        {
            _configurationManager = configurationManager;
            _buildMessages = buildMessages;
        }

        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from file and uses it to build the <see cref="BoundedContextConfiguration"/> using the <see cref="TopologyBuilder"/>.
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies.</param>
        /// <param name="configuration">The <see cref="BuildTaskConfiguration"/>.</param>
        /// <returns>Built <see cref="Applications.Configuration.Topology"/>.</returns>
        public Applications.Configuration.Topology Build(IEnumerable<Type> types, BuildTaskConfiguration configuration)
        {
            var topology = _configurationManager.Load();
            var boundedContextTopology = new BoundedContextTopology(topology, configuration.UseModules, configuration.NamespaceSegmentsToStrip);
            return new TopologyBuilder(types, boundedContextTopology, _buildMessages).Build();
        }

        /// <summary>
        /// Save topology.
        /// </summary>
        /// <param name="topology"><see cref="Applications.Configuration.Topology"/> to save.</param>
        internal void Save(Applications.Configuration.Topology topology)
        {
            _configurationManager.Save(topology);
        }
    }
}