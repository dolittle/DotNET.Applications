using System;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Logging;

namespace Dolittle.Commands
{
    /// <inheritdoc/>
    /// <typeparam name="CommandArtifactType"> Resolving ApplicationArtifactIdentifiers of type <see cref="CommandArtifactType"/></typeparam>
    public class ApplicationArtifactResolverForCommand : ApplicationArtifactResolverFor<CommandArtifactType>
    {
        ILogger _logger;
        public ApplicationArtifactResolverForCommand(
            ILogger logger
        )
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Resolving an {typeof(IApplicationArtifactIdentifier)} for the {typeof(IArtifactType)} {typeof(CommandArtifactType)}");
            return null;
        }
    }
}