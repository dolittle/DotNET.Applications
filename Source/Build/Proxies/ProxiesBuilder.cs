using System;
using Dolittle.Logging;

namespace Dolittle.Build.Proxies
{
     internal class ProxiesBuilder
    {
        readonly Type[] _artifactTypes;
        readonly ILogger _logger;

        internal ProxiesBuilder(Type[] artifactsTypes, ILogger logger)
        {
            _artifactTypes = artifactsTypes;
            _logger = logger;
        }

        internal void Build()
        {
            _logger.Information("Building proxies");
             var startTime = DateTime.UtcNow;

             var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished proxies build process. (Took {deltaTime.TotalSeconds} seconds)");
        }
    }
}