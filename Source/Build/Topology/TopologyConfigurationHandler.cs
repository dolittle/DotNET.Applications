/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications.Configuration;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Logging;
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
        readonly IBoundedContextConfigurationManager _configurationManager;
        readonly ILogger _logger;
        
        /// <summary>
        /// Instantiates an instance of <see cref="TopologyConfigurationHandler"/> 
        /// </summary>
        /// <param name="configurationManager"></param>
        /// <param name="logger"></param>
        public TopologyConfigurationHandler(IBoundedContextConfigurationManager configurationManager, ILogger logger)
        {
            _configurationManager = configurationManager;
            _logger = logger;
        }

        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from file and uses it to build the <see cref="BoundedContextConfiguration"/> using the <see cref="TopologyBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        public BoundedContextConfiguration Build(Type[] types)
        {
            var boundedContextConfiguration = _configurationManager.Load();
            return new TopologyBuilder(types, boundedContextConfiguration, _logger).Build();
        }

        internal void Save(BoundedContextConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}