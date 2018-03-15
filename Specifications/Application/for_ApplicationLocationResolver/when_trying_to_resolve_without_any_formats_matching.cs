using System;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationLocationResolver
{
    public class when_trying_to_resolve_without_any_formats_matching : given.one_format
    {
        static Mock<ISegmentMatches> segment_matches;
        static Exception result;

        Establish context = () => 
        {
            
            segment_matches = new Mock<ISegmentMatches>();
            format.Setup(_ => _.Match(typeof(object).Namespace)).Returns(segment_matches.Object);
            segment_matches.SetupGet(_ => _.HasMatches).Returns(false);
        };

        Because of = () => result = Catch.Exception(() => resolver.Resolve(typeof(object)));

        It should_throw_unable_to_Resolve_application_location_for_type = () => result.ShouldBeOfExactType<UnableToResolveApplicationLocationForType>();
    }
}