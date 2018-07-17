using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_a_Command_and_an_Event_at_different_locations_with_the_same_name : given.resolver_that_knows_about_Commands_and_Events
    {
        static Type command_result;
        static IApplicationArtifactIdentifier command_identifier;
        static Type event_result;
        static IApplicationArtifactIdentifier event_identifier;

        Establish context = () =>
        {
            command_identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.TheCommand.AType));
            event_identifier = aai_to_type_maps.GetIdentifierFor(typeof(given.TheEvent.AType));
        };
        Because of = () =>
        {
            command_result = resolver.Resolve(command_identifier);
            event_result = resolver.Resolve(event_identifier);
        };

        It should_return_the_known_Command_type = () => command_result.ShouldEqual(typeof(given.TheCommand.AType));
        It should_return_the_known_Event_type = () => event_result.ShouldEqual(typeof(given.TheEvent.AType));
    }
}