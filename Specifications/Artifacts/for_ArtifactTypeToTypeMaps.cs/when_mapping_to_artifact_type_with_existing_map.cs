using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps
{

    public class when_mapping_to_artifact_type_with_existing_map : given.one_map
    {
        static IArtifactType result;

        Because of = ()=> result = maps.Map(typeof(ArtifactImplementation));

        It should_return_correct_artifact_type = ()=> result.ShouldEqual(artifact_type_map);
    }
}