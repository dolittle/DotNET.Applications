using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactsResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;
        protected static Mock<IApplicationResourceTypes> application_resource_types;
        protected static Mock<IInstancesOf<ICanResolveApplicationArtifacts>> resolvers;
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IApplicationStructure> application_structure;
        protected static ILogger logger;

        Establish context = () =>
        {
            application_structure = new Mock<IApplicationStructure>();
            application = new Mock<IApplication>();
            application.SetupGet(a => a.Structure).Returns(application_structure.Object);
            application_resource_types = new Mock<IApplicationResourceTypes>();
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationArtifacts>>();
            type_finder = new Mock<ITypeFinder>();
            logger = Mock.Of<ILogger>();
        };
    }
}
