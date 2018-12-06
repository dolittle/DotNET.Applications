using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Build.for_ArtifactsDiscoverer
{
    public class when_discovering_artifacts_with_generic_artifacts : given.all_generic_artifacts_and_their_sub_types
    {
        static IEnumerable<Type> discovered_artifacts;

        Because of = () => discovered_artifacts = new ArtifactsDiscoverer(assemvly_context.Object, dolittle_artifact_types, logger).Artifacts;

        It should_not_contain_any_generic_types = () => discovered_artifacts.ShouldNotContain(generic_types);
        It should_contain_the_non_generic_subtypes_of_the_generic_types = () => discovered_artifacts.ShouldContain(non_generic_subtypes_of_the_generic_types);
    }
}