using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationLocationResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IApplicationStructureMap> application_structure_map;

        Establish context = () => application_structure_map = new Mock<IApplicationStructureMap>();
    }
}