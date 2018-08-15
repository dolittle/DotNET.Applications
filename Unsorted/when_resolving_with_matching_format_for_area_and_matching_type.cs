using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_with_matching_format_for_area_and_matching_type : given.no_resolvers
    {
        class Implementation : IInterface { }

        static Type result;

        Establish context = () =>
        {
            var application_structure = new Mock<IApplicationStructure>();
            var string_format = new Mock<IStringFormat>();
            var segment_matches = new Mock<ISegmentMatches>();
            segment_matches.SetupGet(s => s.HasMatches).Returns(true);
            string_format.Setup(s => s.Match(typeof(Implementation).Namespace)).Returns(segment_matches.Object);
            type_finder.Setup(t => t.FindMultiple(typeof(IInterface))).Returns(new[] { typeof(Implementation) });
        };

        Because of = () => result = resolver.Resolve(identifier.Object);

        It should_return_expected_type = () => result.ShouldEqual(typeof(Implementation));
    }
}
