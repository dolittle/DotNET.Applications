using Dolittle.Artifacts;
using Dolittle.Logging;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class ArtifactResolverForCommand : ApplicationArtifactResolverFor<CommandArtifactType>
    {
        public ArtifactResolverForCommand(
            IApplicationArtifactIdentifierToTypeMaps aaiToTypeMaps, 
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps, 
            ILogger logger) 
            : base(aaiToTypeMaps, artifactTypeToTypeMaps, logger)
        {
        }
    }
}