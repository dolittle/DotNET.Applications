using doLittle.Entities;
using doLittle.Execution;

namespace doLittle.Specs.Configuration
{
    public class EntityContextConnection : IEntityContextConnection
    {
        IContainer _container;

        public void Initialize(IContainer container)
        {
            _container = container;
        }
    }
}
