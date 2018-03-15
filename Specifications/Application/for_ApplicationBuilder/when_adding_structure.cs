using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationConfigurationBuilder
{
    public class when_adding_structure
    {
        const string application_name = "Some Application";
        static ApplicationConfigurationBuilder builder;
        static IApplicationConfigurationBuilder new_builder;
        static IApplicationStructureMapBuilder structure_builder;

        Establish context = () => builder = new ApplicationConfigurationBuilder(application_name);

        Because of = () => new_builder = builder.StructureMappedTo(s => structure_builder = s);

        It should_pass_application_structure_configuration_builder_to_callback = () => structure_builder.ShouldNotBeNull();
    }
}
