using System;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Artifact
{
    internal class ArtifactsConfigurationHandler
    {
        readonly ArtifactsConfigurationManager _configurationManager;
        
        internal ArtifactsConfigurationHandler(ISerializer configurationSerializer)
        {
            _configurationManager = new ArtifactsConfigurationManager(configurationSerializer);
        }

        internal ArtifactsConfiguration Build(Type[] types, ILogger logger)
        {
            return new ArtifactsBuilder(types, _configurationManager, logger).Build();
        }
    }
}