using Bifrost.Configuration;
using Bifrost.Entities;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_ConfigurationExtensions.given
{

    public class a_configuration_element_with_storage
    {
        protected static Mock<IContainer> container_mock;
        protected static IEntityContextConfiguration configuration;
        protected static IEntityContextConnection connection;

        Establish context = () =>
        {
            container_mock = new Mock<IContainer>();
            connection = new EntityContextConnection();
            configuration = new EntityContextConfiguration { Connection = connection, EntityContextType = typeof(EntityContext<>)};
        };
    }
}
