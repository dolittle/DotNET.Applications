using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver
{
    public class when_resolving_with_wrong_artifact_type_mathced : given.no_resolvers
    {

        public class First : IInterface
        {
            public class TheType {}
        }

        public class Second
        {
            public class TheType {}
        }

        static Exception result;


        Establish context = ()=>
        {
            artifact.Setup(_ => _.Name).Returns("TheType");
            artifact_type_to_type_maps.Setup(_ => _.Map(artifact_type.Object)).Returns(typeof(IInterface));
            var types = new[] { typeof(First.TheType), typeof(Second.TheType)};
            type_finder.Setup(_ => _.FindMultiple(typeof(IInterface))).Returns(types);
            application_structure_map.Setup(_ => _.DoesAnyFitInStructure(types)).Returns(true);
            application_structure_map.Setup(_ => _.GetBestMatchingTypeFor(types)).Returns(types[1]);
        };

        Because of = () => result = Catch.Exception(() => resolver.Resolve(identifier.Object));

        It should_throw_artifact_type_mismatch = () => result.ShouldBeOfExactType<MismatchingArtifactType>();
    }
}