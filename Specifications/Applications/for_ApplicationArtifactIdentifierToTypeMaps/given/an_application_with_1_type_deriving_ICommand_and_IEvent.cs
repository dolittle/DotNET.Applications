using System;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps.given
{
    public class an_application_with_1_type_deriving_ICommand_and_IEvent : a_system_finding_Commands_and_Events
    {
        protected static Mock<ITypeFinder> type_finder_for_aai_to_type_maps;
        Establish context = () =>
        {
            type_finder_for_aai_to_type_maps = new Mock<ITypeFinder>();
            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(ICommand))).Returns(new Type[] {typeof(ClassDerivingICommandAndIEvent)});
            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(IEvent))).Returns(new Type[] {typeof(ClassDerivingICommandAndIEvent)});
            
        };
    }
}