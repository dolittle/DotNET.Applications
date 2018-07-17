using System;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class a_aai_and_type_maps_for_Commands_and_Events : a_system_for_finding_Commands_and_Events
    {
        protected static IApplicationArtifactIdentifierAndTypeMaps aai_to_type_maps;
        protected static Mock<ITypeFinder> type_finder_for_aai_to_type_maps;
        Establish context = () =>
        {
            type_finder_for_aai_to_type_maps = new Mock<ITypeFinder>();
            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(ICommand))).Returns(new Type[] {typeof(ACommand), typeof(FalseType), typeof(TheCommand.AType)});

            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(IEvent))).Returns(new Type[] {typeof(AnEvent), typeof(TheEvent.AType)});

            aai_to_type_maps = new ApplicationArtifactIdentifierAndTypeMaps(application_configuration.application, location_resolver, artifact_type_to_type_maps, type_finder_for_aai_to_type_maps.Object);
        }; 
    }
}