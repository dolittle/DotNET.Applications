using doLittle.Mapping;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Mapping.for_MappingTargets.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<IMappingTarget>> mapping_targets_mock;

        Establish context = () => mapping_targets_mock = new Mock<IInstancesOf<IMappingTarget>>();
    }
}
