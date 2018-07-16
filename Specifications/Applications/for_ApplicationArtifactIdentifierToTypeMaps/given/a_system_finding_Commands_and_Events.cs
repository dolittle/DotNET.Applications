using System;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps.given
{
    public class a_system_finding_Commands_and_Events : a_standard_configuration
    {
        protected static Mock<IContainer> container;

        protected static IArtifactType command_artifact_type;
        protected static IArtifactType event_artifact_type;
        protected static IArtifactTypeToTypeMaps artifact_type_to_type_maps;
        Establish context = () => 
        {
            container = new Mock<IContainer>();
            command_artifact_type = new CommandArtifactType();
            event_artifact_type = new EventArtifactType();
            container.Setup(_ => _.Get(typeof(CommandArtifactType))).Returns(command_artifact_type);
            container.Setup(_ => _.Get(typeof(EventArtifactType))).Returns(event_artifact_type);

            type_finder.Setup(_ => _.FindMultiple(typeof(IArtifactTypeMapFor<>))).Returns(new Type[] 
                {
                    typeof(CommandArtifactType),
                    typeof(EventArtifactType)
                });

            artifact_type_to_type_maps = new ArtifactTypeToTypeMaps(type_finder.Object, container.Object);
        };
    }
}