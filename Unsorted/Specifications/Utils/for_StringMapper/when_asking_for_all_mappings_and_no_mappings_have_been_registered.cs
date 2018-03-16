using System.Linq;
using Dolittle.Utils;
using Machine.Specifications;

namespace Dolittle.Utils.for_StringMapper
{
    public class when_asking_for_all_mappings_and_no_mappings_have_been_registered
    {
        static StringMapper mapper;

        Because of = () => mapper = new StringMapper();

        It should_return_an_empty_array = () => mapper.Mappings.Count().ShouldEqual(0);
    }
}
