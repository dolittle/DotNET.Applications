using System.Collections.Generic;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Artifacts.for_ArtifactTypeToTypeMaps.cs.given
{
    public class no_maps : all_dependencies
    {
        protected static ArtifactTypeToTypeMaps maps;

        Establish context = () => 
        {
            providers.Setup(_ => _.GetEnumerator()).Returns(new List<ICanProvideArtifactTypeToTypeMaps>().GetEnumerator());
            maps = new ArtifactTypeToTypeMaps(providers.Object);
        };
    }
}