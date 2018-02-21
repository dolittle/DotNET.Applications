using System;
using Machine.Specifications;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs
{

    public class when_mapping_to_type_with_existing_map : given.one_map
    {
        static Type result;
        
        Because of = () => result = maps.Map(artifact_type);

        It should_return_correct_type = () => result.ShouldEqual(type);
    }        
    
}