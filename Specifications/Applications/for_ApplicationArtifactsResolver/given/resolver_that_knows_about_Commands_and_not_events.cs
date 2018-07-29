using System;
using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class resolver_that_knows_about_Commands_and_not_Events : a_aai_to_type_map_for_Commands_and_not_Events
    {
        protected static ApplicationArtifactResolver resolver;

        Establish context = () =>
        {

            resolver = new ApplicationArtifactResolver(
                aai_to_type_maps,
                artifact_types, 
                artifact_type_to_type_maps,
                logger.Object);
        };
    }
}
