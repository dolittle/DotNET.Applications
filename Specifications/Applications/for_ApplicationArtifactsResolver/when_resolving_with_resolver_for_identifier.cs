using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_with_resolver_for_identifier : given.resolver_that_knows_about_Commands_and_not_Events
    {
        static Type result;
        static IApplicationArtifactIdentifier identifier;

        Establish context = () =>
        {
            identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.ACommand));
        };
        Because of = () => result = resolver.Resolve(identifier);

        It should_return_the_known_type = () => result.ShouldEqual(typeof(given.ACommand));
    }
}
