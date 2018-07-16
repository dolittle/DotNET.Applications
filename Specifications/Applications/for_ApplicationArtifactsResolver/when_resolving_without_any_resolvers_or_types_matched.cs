using System;
using Dolittle.Artifacts;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_without_any_resolvers_or_types_matched : given.no_resolvers
    {
        static Exception exception;
        static IApplicationArtifactIdentifier identifier;

        Establish context = () => identifier = aai_to_type_maps.Map(typeof(given.ACommand));

        Because of = () => exception = Catch.Exception(() => resolver.Resolve(identifier));

        It should_throw_CouldNotFindResolver = () => exception.ShouldBeOfExactType<CouldNotFindResolver>();
    }
}
