using System.Collections.Generic;
using System.Linq;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationLocationResolver
{
    public class when_resolving_that_matches_bounded_context_module_feature_and_subfeature : given.one_format
    {
        const string BoundedContext = "MyBoundedContext";
        const string Module = "MyModule";
        const string Feature = "MyFeature";
        const string TopLevelSubFeature = "MyTopLevelSubFeature";
        const string SecondLevelSubFeature = "MySecondLevelSubFeature";

        static Mock<ISegmentMatch> bounded_context_match;
        static Mock<ISegmentMatch> module_match;
        static Mock<ISegmentMatch> feature_match;
        static Mock<ISegmentMatch> subfeature_match;
        static Mock<ISegmentMatches> segment_matches;
        static IApplicationLocation result;

        Establish context = () => 
        {           
            bounded_context_match = new Mock<ISegmentMatch>();
            bounded_context_match.SetupGet(b => b.Identifier).Returns(ApplicationLocationResolver.BoundedContextKey);
            bounded_context_match.SetupGet(b => b.Values).Returns(new[] { BoundedContext }); 

            module_match = new Mock<ISegmentMatch>();
            module_match.SetupGet(b => b.Identifier).Returns(ApplicationLocationResolver.ModuleKey);
            module_match.SetupGet(b => b.Values).Returns(new[] { Module });

            feature_match = new Mock<ISegmentMatch>();
            feature_match.SetupGet(b => b.Identifier).Returns(ApplicationLocationResolver.FeatureKey);
            feature_match.SetupGet(b => b.Values).Returns(new[] { Feature });

            subfeature_match = new Mock<ISegmentMatch>();
            subfeature_match.SetupGet(b => b.Identifier).Returns(ApplicationLocationResolver.SubFeatureKey);
            subfeature_match.SetupGet(b => b.Values).Returns(new[] { TopLevelSubFeature, SecondLevelSubFeature });

            var segments = new List<ISegmentMatch>(new[]
            {
                bounded_context_match.Object,
                module_match.Object,
                feature_match.Object,
                subfeature_match.Object,
                
            });

            segment_matches = new Mock<ISegmentMatches>();
            segment_matches.SetupGet(m => m.HasMatches).Returns(true);
            segment_matches.Setup(m => m.GetEnumerator()).Returns(segments.GetEnumerator());
            
            format.Setup(_ => _.Match(typeof(object).Namespace)).Returns(segment_matches.Object);
            segment_matches.SetupGet(_ => _.HasMatches).Returns(true);
        };

        Because of = () => result = resolver.Resolve(typeof(object));

        It should_have_five_segments = () => result.Segments.Count().ShouldEqual(5);
        It should_have_bounded_context_as_first_segment = () => result.Segments.ToArray()[0].ShouldBeOfExactType<BoundedContext>();
        It should_have_module_as_second_segment = () => result.Segments.ToArray()[1].ShouldBeOfExactType<Module>();
        It should_have_feature_as_third_segment = () => result.Segments.ToArray()[2].ShouldBeOfExactType<Feature>();
        It should_have_subfeature_as_fourth_segment = () => result.Segments.ToArray()[3].ShouldBeOfExactType<SubFeature>();
        It should_have_subfeature_as_fifth_segment = () => result.Segments.ToArray()[4].ShouldBeOfExactType<SubFeature>();
        It should_hold_the_correct_name_for_bounded_context = () => result.Segments.ToArray()[0].Name.AsString().ShouldEqual(BoundedContext);
        It should_hold_the_correct_name_for_module = () => result.Segments.ToArray()[1].Name.AsString().ShouldEqual(Module);
        It should_hold_the_correct_name_for_feature = () => result.Segments.ToArray()[2].Name.AsString().ShouldEqual(Feature);
        It should_hold_the_correct_name_for_toplevel_subfeature= () => result.Segments.ToArray()[3].Name.AsString().ShouldEqual(TopLevelSubFeature);
        It should_hold_the_correct_name_for_secondlevel_subfeature = () => result.Segments.ToArray()[4].Name.AsString().ShouldEqual(SecondLevelSubFeature);        
    }    
}