using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class resolver_that_knows_about_Commands_and_Events : a_aai_and_type_maps_for_Commands_and_Events
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