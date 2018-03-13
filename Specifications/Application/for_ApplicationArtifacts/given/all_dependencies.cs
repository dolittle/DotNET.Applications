using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifacts.given
{
    public class all_dependencies
    {
        protected const string application_name = "MyApplication";
        protected static Mock<IApplication> application;
        protected static Mock<IApplicationResourceTypes> application_resource_types;

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns(application_name);

            application_resource_types = new Mock<IApplicationResourceTypes>();
        };
    }
}
