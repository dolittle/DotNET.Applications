using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps
{
    public class when_mapping_with_2_different_commands_at_same_location : given.an_application_with_2_different_commands
    {
        static Type type_1;
        static Type type_2;
        static IApplicationArtifactIdentifier result_identifier_for_Register;
        static IApplicationArtifactIdentifier result_identifier_for_Delete;

        static Type result_type_1;
        static Type result_type_2;
        Because of = () => 
        {
            type_1 = typeof(given.Register);
            type_2 = typeof(given.Delete);
            result_identifier_for_Register = aai_to_type_maps.Map(type_1);
            result_identifier_for_Delete = aai_to_type_maps.Map(type_2);
            result_type_1 = aai_to_type_maps.Map(result_identifier_for_Register);
            result_type_2 = aai_to_type_maps.Map(result_identifier_for_Delete);
        };

        It should_not_be_the_same = () => result_identifier_for_Delete.ShouldNotEqual(result_identifier_for_Register);
        It mapping_identifier_for_type_1_should_return_type_1 = () => type_1.ShouldEqual(result_type_1);
        It mapping_identifier_for_type_2_should_return_type_2 = () => type_2.ShouldEqual(result_type_2);
    }
}