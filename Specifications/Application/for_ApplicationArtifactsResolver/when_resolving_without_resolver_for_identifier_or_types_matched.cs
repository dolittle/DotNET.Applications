using System;
using Dolittle.Artifacts;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactsResolver
{

    public class when_resolving_without_resolver_for_identifier_or_types_matched : given.one_resolver_for_known_identifier
    {
        static Exception exception;
        const string other_resource_type_identifier = "OtherResourceType";
        const string other_resource_type_area = "OtherArea";
        const string other_resource_name = "OtherName";

        static Mock<IApplicationArtifactIdentifier> other_identifier;
        static Mock<IArtifact> other_artifact;
        static Mock<IArtifactType> other_artifact_type;

        Establish context = ()=>
        {
            other_artifact_type = new Mock<IArtifactType>();
            other_artifact_type.SetupGet(r => r.Identifier).Returns(other_resource_type_identifier);

            other_artifact = new Mock<IArtifact>();
            other_artifact.SetupGet(r => r.Name).Returns(other_resource_name);
            other_artifact.SetupGet(r => r.Type).Returns(other_artifact_type.Object);

            other_identifier = new Mock<IApplicationArtifactIdentifier>();
            other_identifier.SetupGet(i => i.Artifact).Returns(other_artifact.Object);

            artifact_types.Setup(a => a.GetFor(other_resource_type_identifier)).Returns(other_artifact_type.Object);
        };

        Because of = ()=> exception = Catch.Exception(()=> resolver.Resolve(other_identifier.Object));

        It should_throw_unknown_application_resource_type = ()=> exception.ShouldBeOfExactType<UnknownArtifactType>();
    }
}