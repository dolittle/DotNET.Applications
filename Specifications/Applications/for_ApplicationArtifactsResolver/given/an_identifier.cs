using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class an_identifier : all_dependencies
    {
        protected const string artifact_name = "Implementation";
        protected const string artifact_type_identifier = "MyArtifactType";
        protected static Mock<IApplicationArtifactIdentifier> identifier;
        protected static Mock<IArtifact> artifact;
        protected static Mock<IArtifactType> artifact_type;

        Establish context = () =>
        {
            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(r => r.Identifier).Returns(artifact_type_identifier);

            artifact = new Mock<IArtifact>();
            artifact.SetupGet(r => r.Name).Returns(artifact_name);
            artifact.SetupGet(r => r.Type).Returns(artifact_type.Object);

            identifier = new Mock<IApplicationArtifactIdentifier>();
            identifier.SetupGet(i => i.Artifact).Returns(artifact.Object);

            artifact_types.Setup(a => a.GetFor(artifact_type_identifier)).Returns(artifact_type.Object);
        };
    }
}
