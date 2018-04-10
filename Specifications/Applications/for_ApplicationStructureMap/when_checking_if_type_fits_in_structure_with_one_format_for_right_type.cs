using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationStructureMap
{
    public class when_checking_if_type_fits_in_structure_with_one_format_for_right_type : given.one_format
    {
        static bool result;

        Establish context = () => 
        {
            segment_matches.SetupGet(_ => _.HasMatches).Returns(true);
        };

        Because of = () => result = application_structure_map.DoesFitInStructure(type_for_matching);

        It should_Be_considered_to_fit = () => result.ShouldBeTrue();
    }
}