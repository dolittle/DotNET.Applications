using System;
using Dolittle.Applications.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a class that basically handles the interations with a <see cref="BoundedContextConfiguration"/>
    /// </summary>
    public class TopologyConfigurationHandler
    {
        readonly BoundedContextConfigurationManager _configurationManager;
        
        /// <summary>
        /// Instantiates an instance of <see cref="TopologyConfigurationHandler"/> 
        /// </summary>
        /// <param name="configurationSerializer"></param>
        public TopologyConfigurationHandler(ISerializer configurationSerializer)
        {
            _configurationManager = new BoundedContextConfigurationManager(configurationSerializer);
        }

        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from file and uses it to build the <see cref="BoundedContextConfiguration"/> using the <see cref="TopologyBuilder"/>
        /// </summary>
        /// <param name="types">The discovered artifact types from the bounded context's assemblies</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public BoundedContextConfiguration Build(Type[] types, ILogger logger)
        {
            var boundedContextConfiguration = _configurationManager.Load();
            return new TopologyBuilder(types, boundedContextConfiguration, logger).Build();
        }
        
        internal void Save(BoundedContextConfiguration config)
        {
            _configurationManager.Save(config);
        }
    }
}