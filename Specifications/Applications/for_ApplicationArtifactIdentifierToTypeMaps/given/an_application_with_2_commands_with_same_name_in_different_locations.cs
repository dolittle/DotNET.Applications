using System;
using Dolittle.Commands;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactIdentifierToTypeMaps.given
{
    public class an_application_with_2_commands_with_same_name_in_different_locations : system_finding_CommandArtifactType
    {
        protected static Mock<ITypeFinder> type_finder_for_aai_to_type_maps;
        Establish context = () =>
        {
            type_finder_for_aai_to_type_maps = new Mock<ITypeFinder>();
            type_finder_for_aai_to_type_maps.Setup(_ => _.FindMultiple(typeof(ICommand))).Returns(new Type[] {typeof(given.SubFeature1.Register), typeof(given.SubFeature2.Register)});

        };
    }
}