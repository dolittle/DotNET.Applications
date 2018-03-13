using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanProvideArtifactTypeToTypeMaps>>   providers;

        Establish context = () => providers = new Mock<IInstancesOf<ICanProvideArtifactTypeToTypeMaps>>();
    }
}