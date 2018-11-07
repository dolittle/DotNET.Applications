/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications.Configuration;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a class that handles the interations with a <see cref="BoundedContextConfiguration"/>
    /// </summary>
    public class TopologyConfigurationHandler
    {
        readonly ITopologyConfigurationManager _configurationManager;
        readonly IBuildToolLogger _logger;
        
        /// <summary>
        /// Instantiates an instance of <see cref="TopologyConfigurationHandler"/> 
        /// </summary>
        /// <param name="configurationManager"></param>
        /// <param name="logger"></param>
        public TopologyConfigurationHandler(ITopologyConfigurationManager configurationManager, IBuildToolLogger logger)
        {
            _configurationManager = configurationManager;
            _logger = logger;
        }

        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from file and uses it to build the <see cref="BoundedContextConfiguration"/> using the <see cref="TopologyBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        /// <param name="parsingResults"></param>
        public Applications.Configuration.Topology Build(Type[] types, ArgumentsParsingResult parsingResults)
        {
            var topology = _configurationManager.Load();
            var boundedContextTopology = new BoundedContextTopology(topology, parsingResults.UseModules, parsingResults.NamespaceSegmentsToStrip);
            return new TopologyBuilder(types, boundedContextTopology, _logger).Build();
        }

        internal void Save(Applications.Configuration.Topology topology)
        {
            _configurationManager.Save(topology);
        }
    }
}