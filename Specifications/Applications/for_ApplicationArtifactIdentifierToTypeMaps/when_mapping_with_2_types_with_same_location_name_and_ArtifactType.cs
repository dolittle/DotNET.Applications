using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps
{
    public class when_mapping_with_2_types_with_same_location_name_and_ArtifactType : given.an_application_with_2_types_with_same_name_location_and_ArtifactType
    {
        
        static IApplicationArtifactIdentifierAndTypeMaps aai_to_type_maps;
        static Exception result;

        Establish context = () => 
        {
            
        };
        Because of = () => result = Catch.Exception(() => 
            aai_to_type_maps = new ApplicationArtifactIdentifierAndTypeMaps(application_configuration.application, location_resolver, artifact_type_to_type_maps, type_finder_for_aai_to_type_maps.Object));


        It should_be_a_Ambiguous = () => result.ShouldBeOfExactType(typeof(DuplicateMapping));
    }
}