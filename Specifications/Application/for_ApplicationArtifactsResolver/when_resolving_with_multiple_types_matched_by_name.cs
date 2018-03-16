using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_with_multiple_types_matched_by_name : given.no_resolvers
    {

        public class First
        {
            public class TheType {}
        }

        public class Second
        {
            public class TheType {}
        }

        static Type result;


        Establish context = ()=>
        {
            artifact.Setup(_ => _.Name).Returns("TheType");
            artifact_type_to_type_maps.Setup(_ => _.Map(artifact_type.Object)).Returns(typeof(IInterface));
            var types = new[] { typeof(First.TheType), typeof(Second.TheType)};
            type_finder.Setup(_ => _.FindMultiple(typeof(IInterface))).Returns(types);
            application_structure_map.Setup(_ => _.DoesAnyFitInStructure(types)).Returns(true);
            application_structure_map.Setup(_ => _.GetBestMatchingTypeFor(types)).Returns(types[1]);
        };

        Because of = () => result = resolver.Resolve(identifier.Object);

        It should_return_the_best_matching_type = () => result.ShouldEqual(typeof(Second.TheType));
    }
}