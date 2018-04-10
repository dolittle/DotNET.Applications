using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationLocationResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;
        protected static Mock<IApplicationStructureMap> application_structure_map;

        Establish context = () => 
        {
            application = new Mock<IApplication>();
            application_structure_map = new Mock<IApplicationStructureMap>();
        };
    }
}