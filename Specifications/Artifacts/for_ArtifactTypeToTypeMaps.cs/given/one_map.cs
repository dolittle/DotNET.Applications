using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps.given
{
    public class one_map : all_dependencies
    {
        protected static ArtifactTypeToTypeMaps maps;
        protected static IArtifactType artifact_type;
        protected static Type type;
        protected static ArtifactTypeToType map;

        Establish context = () => 
        {
            artifact_type = Mock.Of<IArtifactType>();
            type = typeof(IUnderlyingArtifact);

            map = new ArtifactTypeToType(artifact_type, type);

            var provider = new Mock<ICanProvideArtifactTypeToTypeMaps>();
            provider.Setup(_ => _.Provide()).Returns(new List<ArtifactTypeToType>(new[] { map }).AsEnumerable());

            providers.Setup(_ => _.GetEnumerator()).Returns(
                new List<ICanProvideArtifactTypeToTypeMaps>(new[] {provider.Object}).GetEnumerator());
            maps = new ArtifactTypeToTypeMaps(providers.Object);
        };

    }
}