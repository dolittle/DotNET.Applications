using System;
using Machine.Specifications;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs
{
    public class when_mapping_to_artifact_type_with_missing_map : given.no_maps
    {
        static Exception result;
        
        Because of = () => result = Catch.Exception(() => maps.Map(typeof(string)));

        It should_throw_missing_artifact_type_for_type = () => result.ShouldBeOfExactType<MissingArtifactTypeForType>();
    }
}