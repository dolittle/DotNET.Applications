using doLittle.Execution;
using doLittle.Tenancy;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Tenancy.for_TenantManager.given
{
    public class all_dependencies
    {
        protected static Mock<ICallContext> call_context;
        protected static Mock<ITenantPopulator> tenant_populator;
        protected static Mock<ICanResolveTenantId> tenant_id_resolver;

        Establish context = () =>
        {
            call_context = new Mock<ICallContext>();
            tenant_populator = new Mock<ITenantPopulator>();
            tenant_id_resolver = new Mock<ICanResolveTenantId>();
        };
    }
}
