using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureMap
{
    public class when_checking_if_type_fits_in_structure_without_any_formats_for_any_areas : given.no_formats
    {
        static bool result;
        Because of = () => result = application_structure_map.DoesFitInStructure(typeof(object));

        It should_be_considered_not_to_fit = () => result.ShouldBeFalse();        
    }
}