using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class one_resolver_for_known_identifier : an_identifier
    {
        protected static ApplicationArtifactResolver resolver;

        protected static Mock<ICanResolveApplicationArtifacts> resource_resolver;

        protected static Type known_type = typeof(an_identifier);

        Establish context = () =>
        {
            resource_resolver = new Mock<ICanResolveApplicationArtifacts>();
            resource_resolver.SetupGet(r => r.ArtifactType).Returns(artifact_type.Object);
            resource_resolver.Setup(r => r.Resolve(identifier.Object)).Returns(known_type);

            resolvers.Setup(r => r.GetEnumerator()).Returns(
                new List<ICanResolveApplicationArtifacts>(
                    new[] { resource_resolver.Object }).GetEnumerator()
                );

            resolver = new ApplicationArtifactResolver(
                artifact_types.Object, 
                artifact_type_to_type_maps.Object,
                resolvers.Object,
                logger);
        };
    }
}
