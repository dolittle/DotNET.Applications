using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactsResolver.given
{
    public class an_identifier : all_dependencies
    {
        protected const string artifact_name = "Implementation";
        protected const string artifact_type_identifier = "MyArtifactType";
        protected static ApplicationArea area = "MyArea";

        protected static Mock<IApplicationArtifactIdentifier> identifier;
        protected static Mock<IArtifact> artifact;
        protected static Mock<IArtifactType> artifact_type;

        Establish context = () =>
        {
            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(r => r.Identifier).Returns(resource_type_identifier);
            artifact_type.SetupGet(r => r.Type).Returns(typeof(IInterface));
            artifact_type.SetupGet(r => r.Area).Returns(area);

            artifact = new Mock<IApplicationResource>();
            artifact.SetupGet(r => r.Name).Returns(artifact_name);
            artifact.SetupGet(r => r.Type).Returns(artifact_type.Object);
            

            identifier = new Mock<IArtifact>();
            identifier.SetupGet(i => i.Resource).Returns(artifact.Object);

            application_artifact_types.Setup(a => a.GetFor(artifact_type_identifier)).Returns(artifact_type.Object);
        };
    }
}
