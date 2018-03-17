namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps
{
    public class ArtifactType : IArtifactTypeMapFor<IUnderlyingArtifact>
    {
        public ArtifactType(string identifier)=> Identifier = identifier;
        public string Identifier { get; } = "Underlying";
    }
}