using System;
using Dolittle.Artifacts;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps
{
    public class when_mapping_with_2_commands_with_same_name_at_different_locations : given.an_application_with_2_commands_with_same_name_in_different_locations
    {
        static IApplicationArtifactIdentifierToTypeMaps aai_to_type_maps;

        static Type type_1;
        static Type type_2;
        static IApplicationArtifactIdentifier result_identifier_for_SubFeature1Register;
        static IApplicationArtifactIdentifier result_identifier_for_SubFeature2Register;
        static Type result_type_1;
        static Type result_type_2;
        
        static IApplicationArtifactIdentifier bad_identifier;
        static Exception result_exception;

        Establish context = () => {
            aai_to_type_maps = new ApplicationArtifactIdentifierToTypeMaps(application_configuration.application, location_resolver, artifact_type_to_type_maps, type_finder_for_aai_to_type_maps.Object);

        };

        Because of = () => 
        {
            type_1 = typeof(given.SubFeature1.Register);
            type_2 = typeof(given.SubFeature2.Register);

            result_identifier_for_SubFeature1Register = aai_to_type_maps.Map(type_1);
            result_identifier_for_SubFeature2Register = aai_to_type_maps.Map(type_2);

            result_type_1 = aai_to_type_maps.Map(result_identifier_for_SubFeature1Register);
            result_type_2 = aai_to_type_maps.Map(result_identifier_for_SubFeature2Register);

            bad_identifier = new ApplicationArtifactIdentifier(
                result_identifier_for_SubFeature1Register.Application, 
                result_identifier_for_SubFeature1Register.Location, 
                new Artifact("BadName", command_artifact_type, 1));

            result_exception = Catch.Exception(() => aai_to_type_maps.Map(bad_identifier));

        };

        It identifiers_should_not_be_the_same = () => result_identifier_for_SubFeature1Register.ShouldNotEqual(result_identifier_for_SubFeature2Register);
        It mapping_identifier_for_type_1_should_return_type_1 = () => type_1.ShouldEqual(result_type_1);
        It mapping_identifier_for_type_2_should_return_type_2 = () => type_2.ShouldEqual(result_type_2);
        It should_throw_CouldNotResolveApplicationArtifactIdentifier_when_mapping_bad_identifier = () => result_exception.ShouldBeOfExactType(typeof(CouldNotResolveApplicationArtifactIdentifier));
    }
}