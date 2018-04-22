using System;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps
{
    public class when_mapping_to_artifact_type_with_inheritance_and_heritance_chain_contains_multiple_candidates : given.no_maps
    {
        protected const string first_level_identifier = "FirstLevelType";
        protected const string second_level_identifier = "FirstLevelType";

        protected static ArtifactType first_level_artifact_type;
        protected static ArtifactType second_level_artifact_type;

        interface IFirstLevelUnderlyingType {}
        interface ISecondLevelUnderlyingType : IFirstLevelUnderlyingType {}

        class FirstLevelArtifactType : IArtifactTypeMapFor<IFirstLevelUnderlyingType>
        {
            public string Identifier { get; } = first_level_identifier;
        }

        class SecondLevelArtifactType : IArtifactTypeMapFor<ISecondLevelUnderlyingType>
        {
            public string Identifier { get; } = second_level_identifier;
        }

        class SecondLevelArtifactTypeImplementation : ISecondLevelUnderlyingType {}

        static IArtifactType result;

        Establish context = ()=>
        {
            first_level_artifact_type = new ArtifactType(first_level_identifier);
            second_level_artifact_type = new ArtifactType(second_level_identifier);

            type_finder.Setup(_ => _.FindMultiple(typeof(IArtifactTypeMapFor<>))).Returns(new Type[] {  
                typeof(FirstLevelArtifactType),
                typeof(SecondLevelArtifactType)
            });

            container.Setup(_ => _.Get(typeof(FirstLevelArtifactType))).Returns(first_level_artifact_type);
            container.Setup(_ => _.Get(typeof(SecondLevelArtifactType))).Returns(second_level_artifact_type);

            maps = new ArtifactTypeToTypeMaps(type_finder.Object, container.Object);
        };

        Because of = () => {
            result = maps.Map(typeof(SecondLevelArtifactTypeImplementation));
        };

        It should_return_correct_artifact_type = () => result.ShouldEqual(second_level_artifact_type);
    }
}