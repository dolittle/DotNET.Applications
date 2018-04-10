using Dolittle.Strings;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationLocationResolver.given
{
    public class one_format : all_dependencies
    {
        protected static Mock<IStringFormat> format;

        protected static ApplicationLocationResolver resolver;

        Establish context = () => 
        {
            format = new Mock<IStringFormat>();
            application_structure_map.SetupGet(_ => _.Formats).Returns(new[] { format.Object });
            resolver = new ApplicationLocationResolver(
                application.Object,
                application_structure_map.Object
            );
        };
    }
}