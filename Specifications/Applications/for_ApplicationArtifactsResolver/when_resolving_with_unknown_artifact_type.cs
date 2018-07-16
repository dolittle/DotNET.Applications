using System;
using Dolittle.Artifacts;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{

    public class when_resolving_with_unknown_artifact_type : given.one_resolver_for_known_identifier
    {
        static Exception exception;
        static IApplicationArtifactIdentifier identifier;
        Establish context = ()=>
        {
            identifier = aai_to_type_maps.Map(typeof(given.AnEvent));
        };

        Because of = ()=> exception = Catch.Exception(()=> resolver.Resolve(identifier));

        It should_throw_unknown_application_resource_type = ()=> exception.ShouldBeOfExactType<UnknownArtifactType>();
    }
}