using Dolittle.Applications;
using Machine.Specifications;
using Moq;

namespace Dolittle.Runtime.Commands.Specs.for_CommandRequestConverter.given
{
    public class all_dependencies
    {
        protected static Mock<IApplicationResourceResolver> application_resource_resolver;

        Establish context = () => application_resource_resolver = new Mock<IApplicationResourceResolver>();
    }
}
