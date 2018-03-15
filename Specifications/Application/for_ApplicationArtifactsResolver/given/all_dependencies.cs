using Dolittle.Artifacts;
using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactsResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IApplicationStructureMap> application_structure_map;
        protected static Mock<IArtifactTypes> artifact_types;
        protected static Mock<IArtifactTypeToTypeMaps> artifact_type_to_type_maps;
        protected static Mock<IInstancesOf<ICanResolveApplicationArtifacts>> resolvers;
        protected static Mock<ITypeFinder> type_finder;
        protected static ILogger logger;

        Establish context = () =>
        {
            artifact_type_to_type_maps = new Mock<IArtifactTypeToTypeMaps>();
            application_structure_map = new Mock<IApplicationStructureMap>();
            artifact_types = new Mock<IArtifactTypes>();
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationArtifacts>>();
            type_finder = new Mock<ITypeFinder>();
            logger = Mock.Of<ILogger>();
        };
    }
}
