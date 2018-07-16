using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps
{
    public class when_mapping_with_2_commands_with_same_name_at_different_locations : given.an_application_with_2_commands_with_same_name_in_different_locations
    {
        static IApplicationArtifactIdentifier result_identifier_for_SubFeature1Register;
        static IApplicationArtifactIdentifier result_identifier_for_SubFeature2Register;
        Because of = () => 
        {
            result_identifier_for_SubFeature1Register = aai_to_type_maps.Map(typeof(given.SubFeature1.Register));
            result_identifier_for_SubFeature2Register = aai_to_type_maps.Map(typeof(given.SubFeature2.Register));
        };

        It should_not_be_the_same = () => result_identifier_for_SubFeature1Register.ShouldNotEqual(result_identifier_for_SubFeature2Register);
    }
}