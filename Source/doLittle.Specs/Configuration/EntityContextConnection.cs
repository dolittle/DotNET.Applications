using Bifrost.Entities;
using Bifrost.Execution;

namespace Bifrost.Specs.Configuration
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
