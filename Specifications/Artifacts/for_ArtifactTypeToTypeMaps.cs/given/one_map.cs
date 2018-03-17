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
        protected const string identifier = "SomeArtifactType";
        protected static ArtifactTypeToTypeMaps maps;
        protected static ArtifactType artifact_type_map;
        Establish context = () => 
        {
            artifact_type_map = new ArtifactType(identifier);

            var mapType = typeof(ArtifactType);

            type_finder.Setup(_ => _.FindMultiple(typeof(IArtifactTypeMapFor<>))).Returns(new Type[] { mapType });
            container.Setup(_ => _.Get(mapType)).Returns(artifact_type_map);

            maps = new ArtifactTypeToTypeMaps(type_finder.Object, container.Object);
        };
    }
}