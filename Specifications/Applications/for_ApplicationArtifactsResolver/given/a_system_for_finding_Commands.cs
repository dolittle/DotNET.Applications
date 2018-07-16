using System;
using System.Collections.Generic;
using Dolittle.Artifacts;
using Dolittle.DependencyInversion;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class a_system_for_finding_Commands : a_standard_configuration
    {
        protected static Mock<IContainer> container;
        protected static IArtifactType command_artifact_type;
        protected static IArtifactType event_artifact_type;
        protected static IArtifactTypeToTypeMaps artifact_type_to_type_maps;
        protected static IArtifactTypes artifact_types;
        protected static Mock<IInstancesOf<IArtifactType>> the_types;
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

            the_types = new Mock<IInstancesOf<IArtifactType>>();
            // Setup types to only recognize ICommand
            the_types.Setup(_ => _.GetEnumerator()).Returns(new List<IArtifactType>(){command_artifact_type}.GetEnumerator());
            artifact_types = new ArtifactTypes(the_types.Object);
        };
    }
}