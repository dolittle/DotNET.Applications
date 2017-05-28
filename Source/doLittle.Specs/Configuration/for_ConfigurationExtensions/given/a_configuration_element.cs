using doLittle.Configuration;
using doLittle.Entities;
using doLittle.Execution;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Configuration.for_ConfigurationExtensions.given
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
