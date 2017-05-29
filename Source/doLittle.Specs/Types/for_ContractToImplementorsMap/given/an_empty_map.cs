using doLittle.Types;
using Machine.Specifications;

namespace doLittle.Specs.Types.for_ContractToImplementorsMap.given
{
    public class an_empty_map
    {
        protected static ContractToImplementorsMap map;

        Establish context = () => map = new ContractToImplementorsMap();
    }
}
