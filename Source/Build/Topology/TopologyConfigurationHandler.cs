using System;
using Dolittle.Applications.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Topology
{
    internal class TopologyConfigurationHandler
    {
        readonly BoundedContextConfigurationManager _configurationManager;
        
        internal TopologyConfigurationHandler(ISerializer configurationSerializer)
        {
            _configurationManager = new BoundedContextConfigurationManager(configurationSerializer);
        }

        internal BoundedContextConfiguration Build(Type[] types, IBoundedContextConfigurationManager boundedContextConfigurationManager, ILogger logger)
        {
            return new TopologyBuilder(types, boundedContextConfigurationManager, logger).Build();
        }
    }
}