using System;
using Dolittle.Applications.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Topology
{
    internal class TopologyBuilder
    {
        readonly Type[] _artifactTypes;
        readonly ILogger _logger;
        readonly IBoundedContextConfigurationManager _boundedContextConfigurationManager;


        internal TopologyBuilder(Type[] artifactsTypes, IBoundedContextConfigurationManager boundedContextConfigurationManager, ILogger logger)
        {
            _artifactTypes = artifactsTypes;
            _logger = logger;

            _boundedContextConfigurationManager = boundedContextConfigurationManager;
        }

        internal BoundedContextConfiguration BuildTopology()
        {
            var boundedContext = _boundedContextConfigurationManager.Load();
            _logger.Information("Building topology");
             var startTime = DateTime.UtcNow;

            

             var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished topology build process. (Took {deltaTime.TotalSeconds} seconds)");

            return boundedContext;
        }
    }
}