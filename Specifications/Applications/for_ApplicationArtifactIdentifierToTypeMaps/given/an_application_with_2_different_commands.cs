using System;
using Dolittle.Commands;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps.given
{
    public class an_application_with_2_different_commands : system_finding_CommandArtifactType
    {
        protected static IApplicationArtifactIdentifierAndTypeMaps aai_to_type_maps;
        protected static Mock<ITypeFinder> type_finder_for_aai_to_type_maps;
        Establish context = () =>
        {
            type_finder_for_aai_to_type_maps = new Mock<ITypeFinder>();
            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(ICommand))).Returns(new Type[] {typeof(Register), typeof(Delete)});

            aai_to_type_maps = new ApplicationArtifactIdentifierAndTypeMaps(application_configuration.application, location_resolver, artifact_type_to_type_maps, type_finder_for_aai_to_type_maps.Object);
        };
    }
}