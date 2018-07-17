using System.Collections.Generic;
using System.Linq;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationLocationResolver
{
    public class when_resolving_that_matches_bounded_context_and_module : given.one_format
    {
        const string BoundedContext = "MyBoundedContext";
        const string Module = "MyModule";
        static Mock<ISegmentMatches> segment_matches;
        static Mock<ISegmentMatch> bounded_context_match;
        static Mock<ISegmentMatch> module_match;
        static IApplicationLocation result;

        Establish context = () =>
        {
            bounded_context_match = new Mock<ISegmentMatch>();
            bounded_context_match.SetupGet(b => b.Identifier).Returns(ApplicationStructureMap.BoundedContextKey);
            bounded_context_match.SetupGet(b => b.Values).Returns(new[] { BoundedContext }); 

            module_match = new Mock<ISegmentMatch>();
            module_match.SetupGet(b => b.Identifier).Returns(ApplicationStructureMap.ModuleKey);
            module_match.SetupGet(b => b.Values).Returns(new[] { Module });

            var segments = new List<ISegmentMatch>(new[]
            {
                bounded_context_match.Object,
                module_match.Object
            });

            segment_matches = new Mock<ISegmentMatches>();
            segment_matches.SetupGet(m => m.HasMatches).Returns(true);
            segment_matches.Setup(m => m.GetEnumerator()).Returns(segments.GetEnumerator());

            format.Setup(_ => _.Match(typeof(object).Namespace)).Returns(segment_matches.Object);
            segment_matches.SetupGet(_ => _.HasMatches).Returns(true);

        };

        Because of = () => result = resolver.Resolve(typeof(object));

        It should_have_two_segments = () => result.Segments.Count().ShouldEqual(2);
        It should_have_bounded_context_as_first_segment = () => result.Segments.ToArray()[0].ShouldBeOfExactType<BoundedContext>();
        It should_have_module_as_second_segment = () => result.Segments.ToArray()[1].ShouldBeOfExactType<Module>();
        It should_hold_the_correct_name_for_bounded_context = () => result.Segments.ToArray()[0].Name.AsString().ShouldEqual(BoundedContext);
        It should_hold_the_correct_name_for_module = () => result.Segments.ToArray()[1].Name.AsString().ShouldEqual(Module);
    }
}
