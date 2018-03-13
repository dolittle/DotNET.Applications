using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactsResolver.given
{
    public class no_resolvers : an_identifier
    {
        protected static ApplicationArtifactResolver resolver;

        Establish context = () =>
        {
            resolvers.Setup(r => r.GetEnumerator()).Returns(new List<ICanResolveApplicationArtifacts>().GetEnumerator());
            resolver = new ApplicationArtifactResolver(
                application.Object,
                application_artifact_types.Object, 
                resolvers.Object,
                type_finder.Object,
                logger);
        };
    }
}
