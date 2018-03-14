using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps
{
    public class when_mapping_to_type_with_missing_map : given.no_maps
    {
        static Exception result;

        static Mock<IArtifactType> artifact_type;

        Establish context = ()=>
        {
            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(_ => _.Identifier).Returns("SomeArtifact");
        };

        Because of = ()=> result = Catch.Exception(()=> maps.Map(artifact_type.Object));

        It should_throw_missing_type_for_artifact_type = ()=> result.ShouldBeOfExactType<MissingTypeForArtifactType>();
    }

}