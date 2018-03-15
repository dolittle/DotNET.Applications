using System.Linq;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationStructureMapBuilder
{
    public class when_building_with_two_added_structure_format_with_different_identifiers
    {
        const string first_area = "FirstArea";
        const string second_area = "SecondArea";
        const string first_structure_format = "[.]FirstFormat";
        const string second_structure_format = "[.]SecondFormat";
        static IApplicationStructureMapBuilder builder;
        static IApplicationStructureMap structure;

        static Mock<IApplication> application;

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            var b = new ApplicationStructureMapBuilder();

            builder = b.Include(first_area, first_structure_format).Include(second_area, second_structure_format);
        };

        Because of = () => structure = builder.Build(application.Object);

        It should_hold_two_structure_formats = () => structure.Formats.Count().ShouldEqual(2);
    }
}
