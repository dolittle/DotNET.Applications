using Dolittle.Artifacts;
using Dolittle.Events;
namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps.given
{
    public class EventArtifactType : IArtifactTypeMapFor<IEvent>
    {
        /// <inheritdoc/>
        public string Identifier => "Event";
    }
}