using doLittle.Configuration;
using doLittle.Entities;
using Machine.Specifications;
using System;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Configuration.for_ConfigurationExtensions
{
    [Subject(typeof(ConfigurationStorageElement))]
    public class when_initializing_for_default_storage : given.a_configuration_element_with_storage
    {
        static Type default_type;
        Establish context = () =>
        {
            default_type = typeof(EntityContext<>);
        };


        Because of = () => configuration.BindDefaultEntityContext(container.Object);

        It should_bind_the_default_connection_connection = () => container.Verify(c => c.Bind(typeof(EntityContextConnection), connection));
        It should_bind_to_the_default_storage_type = () => container.Verify(c => c.Bind(typeof(IEntityContext<>),default_type));
        It should_not_make_any_other_calls_on_the_container = () => container.VerifyAll();
      
    }
}
