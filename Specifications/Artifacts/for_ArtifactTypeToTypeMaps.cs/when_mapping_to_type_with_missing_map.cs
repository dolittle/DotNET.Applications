using System;
using Machine.Specifications;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs
{
    public class when_mapping_to_type_with_missing_map : given.no_maps
    {
        static Exception result;
        
        Because of = () => result = Catch.Exception(() => maps.Map(Moq.Mock.Of<IArtifactType>()));

        It should_throw_missing_type_for_artifact_type = () => result.ShouldBeOfExactType<MissingTypeForArtifactType>();
    }    
   
}