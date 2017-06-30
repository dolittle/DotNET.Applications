using System.Collections.Generic;
using doLittle.Applications;
using Machine.Specifications;

namespace doLittle.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class no_resolvers : an_identifier
    {
        protected static ApplicationResourceResolver resolver;

        Establish context = () =>
        {
            resolvers.Setup(r => r.GetEnumerator()).Returns(new List<ICanResolveApplicationResources>().GetEnumerator());
            resolver = new ApplicationResourceResolver(
                application.Object,
                application_resource_types.Object, 
                resolvers.Object,
                type_finder.Object,
                logger);
        };
    }
}
