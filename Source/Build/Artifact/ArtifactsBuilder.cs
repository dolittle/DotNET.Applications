using System;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Artifact
{
     internal class ArtifactsBuilder
    {
        readonly Type[] _artifactTypes;
        readonly ILogger _logger;

        ArtifactsConfiguration _artifactsConfiguration;

        internal ArtifactsBuilder(Type[] artifactsTypes, ArtifactsConfiguration artifactsConfiguration, ILogger logger)
        {
            _artifactTypes = artifactsTypes;
            _logger = logger;

            _artifactsConfiguration = artifactsConfiguration;

        }

        internal ArtifactsConfiguration Build()
        {
            _logger.Information("Building artifacts");
             var startTime = DateTime.UtcNow;

            
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished artifacts build process. (Took {deltaTime.TotalSeconds} seconds)");
            
            return _artifactsConfiguration;
        }
    }
}