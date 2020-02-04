// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Build.for_ArtifactsDiscoverer
{
    public class when_constructing_artifacts_discoverer_with_abstract_artifacts : given.all_abstract_artifacts_and_their_sub_types
    {
        static IEnumerable<Type> discovered_artifacts;

        Because of = () => discovered_artifacts = new ArtifactsDiscoverer(assembly_context.Object, dolittle_artifact_types, build_messages).Artifacts;

        It should_not_contain_any_abstract_types = () => discovered_artifacts.ShouldNotContain(abstract_types);
        It should_contain_the_non_abstract_subtypes_of_the_abstract_types = () => discovered_artifacts.ShouldContain(non_abstract_subtypes_of_the_abstract_types);
    }
}