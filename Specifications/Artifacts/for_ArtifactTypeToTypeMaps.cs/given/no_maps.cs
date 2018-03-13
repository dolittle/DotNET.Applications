using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps.given
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