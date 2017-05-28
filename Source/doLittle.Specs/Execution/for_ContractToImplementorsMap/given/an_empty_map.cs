using doLittle.Execution;
using Machine.Specifications;

namespace doLittle.Specs.Execution.for_ContractToImplementorsMap.given
{
    public class an_empty_map
    {
        protected static ContractToImplementorsMap map;

        Establish context = () => map = new ContractToImplementorsMap();
    }
}
