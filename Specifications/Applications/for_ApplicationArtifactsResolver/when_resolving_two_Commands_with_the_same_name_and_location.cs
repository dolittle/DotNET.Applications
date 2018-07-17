using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_two_Commands_with_the_same_name_and_location : given.resolver_that_knows_about_Commands_and_not_Events
    {
        static Exception command1_result;
        static IApplicationArtifactIdentifier command1_identifier;
        static Exception command2_result;
        static IApplicationArtifactIdentifier command2_identifier;

        Establish context = () =>
        {
            command1_identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.First.TheType));
            command2_identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.Second.TheType));
        };

        Because of = () =>
        {
            command1_result = Catch.Exception(() => resolver.Resolve(command1_identifier));
            command2_result = Catch.Exception(() => resolver.Resolve(command2_identifier));
        };

        It should_return_the_first_known_Command_type = () => command1_result.ShouldBeOfExactType(typeof(MultipleTypesWithTheSameArtifactIdentifier));
        It should_return_the_second_known_Command_type = () => command2_result.ShouldBeOfExactType(typeof(MultipleTypesWithTheSameArtifactIdentifier));
    }
}