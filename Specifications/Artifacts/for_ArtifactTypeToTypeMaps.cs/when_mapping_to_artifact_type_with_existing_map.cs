using System;
using Machine.Specifications;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs
{

    public class when_mapping_to_artifact_type_with_existing_map : given.one_map
    {
        static IArtifactType result;
        
        Because of = () => result = maps.Map(type);

        It should_return_correct_artifact_type = () => result.ShouldEqual(artifact_type);
    }        
    
}