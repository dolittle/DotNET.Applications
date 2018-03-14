using System;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps.given
{
    public class no_maps : all_dependencies
    {
        protected static ArtifactTypeToTypeMaps maps;

        Establish context = () => 
        {
            type_finder.Setup(_ => _.FindMultiple(typeof(IArtifactTypeMapFor<>))).Returns(new Type[0]);
            maps = new ArtifactTypeToTypeMaps(type_finder.Object, container.Object);
        };
    }
}