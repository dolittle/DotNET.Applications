using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationConfigurationBuilder
{
    public class when_building
    {
        const string application_name = "Some Application";

        static ApplicationConfigurationBuilder builder;
        static (IApplication application, IApplicationStructureMap structureMap) result;
        static Mock<IApplicationStructureMapBuilder> application_structure_map_builder;
        static Mock<IApplicationStructureMap> application_structure_map;

        Establish context = () =>
        {
            application_structure_map = new Mock<IApplicationStructureMap>();
            application_structure_map_builder = new Mock<IApplicationStructureMapBuilder>();
            application_structure_map_builder.Setup(a => a.Build(Moq.It.IsAny<IApplication>())).Returns(application_structure_map.Object);
            builder = new ApplicationConfigurationBuilder(application_name, application_structure_map_builder.Object);
        };

        Because of = () => result = builder.Build();

        It should_return_an_application = () => result.application.ShouldNotBeNull();
        It should_return_the_built_structure_map = () => result.structureMap.ShouldEqual(application_structure_map.Object);
        It should_hold_the_name_of_the_application = () => ((string) result.application.Name).ShouldEqual(application_name);
        It should_build_application_structure = () => application_structure_map_builder.Verify(a => a.Build(Moq.It.IsAny<IApplication>()), Times.Once());
    }
}
