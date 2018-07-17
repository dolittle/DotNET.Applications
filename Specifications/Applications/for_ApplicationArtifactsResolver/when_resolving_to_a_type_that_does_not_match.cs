using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_to_a_type_that_does_not_match : given.resolver_that_knows_about_Commands_and_not_Events
    {
        
        static Exception exception;
        static IApplicationArtifactIdentifier identifier;
        Establish context = ()=>
        {
            identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.FalseType));
        };

        Because of = ()=> exception = Catch.Exception(()=> resolver.Resolve(identifier));

        // It should_throw_MismatchingArtifactType = ()=> exception.ShouldBeOfExactType<MismatchingArtifactType>();
    }
}