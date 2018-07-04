using System;
using Dolittle.Strings;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationStructureMap.given
{
    public class one_format : all_dependencies
    {
        protected static Mock<IStringFormat> string_format;
        protected static Mock<ISegmentMatches> segment_matches;
        protected static ApplicationStructureMap application_structure_map;

        protected static Type type_for_matching;

        Establish context = ()=>
        {
            type_for_matching = typeof(Object);
            string_format = new Mock<IStringFormat>();
            segment_matches = new Mock<ISegmentMatches>();
            string_format.Setup(_ => _.Match("System.Object")).Returns(segment_matches.Object);
            formats = new [] {  string_format.Object };

            application_structure_map = new ApplicationStructureMap(application.Object, formats);
        };
    }
}