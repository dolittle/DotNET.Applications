using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureMap.given
{
    public class no_formats : all_dependencies
    {
        protected static ApplicationStructureMap application_structure_map;

        Establish context = () => application_structure_map = new ApplicationStructureMap(application.Object, formats_per_area);
    }
}