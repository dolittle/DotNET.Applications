using doLittle.DependencyInversion;
using doLittle.Entities;

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
