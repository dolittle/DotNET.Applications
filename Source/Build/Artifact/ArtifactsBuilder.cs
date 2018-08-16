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

        readonly IArtifactsConfigurationManager _artifactsConfigurationManager;
        internal ArtifactsBuilder(Type[] artifactsTypes, ILogger logger, ISerializer serializer)
        {
            _artifactTypes = artifactsTypes;
            _logger = logger;

            _artifactsConfigurationManager = new ArtifactsConfigurationManager(serializer);

        }

        internal ArtifactsConfiguration BuildArtifacts()
        {
            var artifactsConfiguration = _artifactsConfigurationManager.Load();

            _logger.Information("Building artifacts");
             var startTime = DateTime.UtcNow;

            
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished artifacts build process. (Took {deltaTime.TotalSeconds} seconds)");
            
            return artifactsConfiguration;
        }
    }
}