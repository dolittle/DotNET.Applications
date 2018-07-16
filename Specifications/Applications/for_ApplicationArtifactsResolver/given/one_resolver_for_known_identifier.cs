using System;
using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class one_resolver_for_known_identifier : a_aai_to_type_map_for_Commands
    {
        protected static ApplicationArtifactResolver resolver;
        protected static Mock<IInstancesOf<ICanResolveApplicationArtifacts>> resolvers;
        protected static ICanResolveApplicationArtifacts command_resolver;

        Establish context = () =>
        {
            command_resolver = new ArtifactResolverForCommand(aai_to_type_maps, artifact_type_to_type_maps, logger.Object);
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationArtifacts>>();
            resolvers.Setup(r => r.GetEnumerator()).Returns(new List<ICanResolveApplicationArtifacts>(){command_resolver}.GetEnumerator());

            resolver = new ApplicationArtifactResolver(
                artifact_types, 
                artifact_type_to_type_maps,
                resolvers.Object,
                logger.Object);
        };
    }
}
