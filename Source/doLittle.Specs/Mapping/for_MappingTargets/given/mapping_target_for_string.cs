using System.Linq;
using doLittle.Mapping;
using Machine.Specifications;

namespace doLittle.Specs.Mapping.for_MappingTargets.given
{
    public class mapping_target_for_string : all_dependencies
    {
        protected static StringMappingTarget mapping_target;
        protected static MappingTargets targets;

        Establish context = () =>
        {
            mapping_target = new StringMappingTarget();
            mapping_targets_mock.Setup(m => m.GetEnumerator()).Returns(new[] { mapping_target }.AsEnumerable().GetEnumerator());
            targets = new MappingTargets(mapping_targets_mock.Object);
        };
    }
}
