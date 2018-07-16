using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class no_resolvers : a_aai_to_type_map_for_Commands
    {
        protected static ApplicationArtifactResolver resolver;
        protected static Mock<IInstancesOf<ICanResolveApplicationArtifacts>> resolvers;

        Establish context = () =>
        {
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationArtifacts>>();
            resolvers.Setup(r => r.GetEnumerator()).Returns(new List<ICanResolveApplicationArtifacts>().GetEnumerator());
            resolver = new ApplicationArtifactResolver(artifact_types, artifact_type_to_type_maps, resolvers.Object, logger.Object);
        };
    }
}