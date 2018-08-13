using System.Collections.Generic;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class ArtifactsConfigurationExtensions
    {
        internal static IEnumerable<ArtifactDefinition> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration)
        {
            var featureDefinitions = new List<ArtifactDefinition>();

            foreach (var artifactEntry in configuration.Artifacts)
            {
                featureDefinitions.AddRange(artifactEntry.Value.Commands);
                featureDefinitions.AddRange(artifactEntry.Value.EventProcessors);
                featureDefinitions.AddRange(artifactEntry.Value.Events);
                featureDefinitions.AddRange(artifactEntry.Value.EventSources);
                featureDefinitions.AddRange(artifactEntry.Value.Queries);
                featureDefinitions.AddRange(artifactEntry.Value.ReadModels);
                
            }
            return featureDefinitions;
        }
    }
}