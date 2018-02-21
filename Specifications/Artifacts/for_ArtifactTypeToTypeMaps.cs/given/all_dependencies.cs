using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanProvideArtifactTypeToTypeMaps>>   providers;

        Establish context = () => providers = new Mock<IInstancesOf<ICanProvideArtifactTypeToTypeMaps>>();
    }
}