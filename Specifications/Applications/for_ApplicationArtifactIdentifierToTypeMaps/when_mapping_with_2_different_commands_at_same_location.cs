using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps
{
    public class when_mapping_with_2_different_commands_at_same_location : given.an_application_with_2_different_commands
    {
        static IApplicationArtifactIdentifier result_identifier_for_Register;
        static IApplicationArtifactIdentifier result_identifier_for_Delete;
        Because of = () => 
        {
            result_identifier_for_Register = aai_to_type_maps.Map(typeof(given.Register));
            result_identifier_for_Delete = aai_to_type_maps.Map(typeof(given.Delete));
        };

        It should_not_be_the_same = () => result_identifier_for_Delete.ShouldNotEqual(result_identifier_for_Register);
    }
}