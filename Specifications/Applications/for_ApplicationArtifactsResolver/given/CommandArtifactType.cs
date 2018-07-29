using Dolittle.Artifacts;
using Dolittle.Commands;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    /// <summary>
    /// Represents the identifier and map to the concrete <see cref="ICommand"/> type
    /// </summary>
    public class CommandArtifactType : IArtifactTypeMapFor<ICommand>
    {
        /// <inheritdoc/>
        public string Identifier => "Command";
    }
}