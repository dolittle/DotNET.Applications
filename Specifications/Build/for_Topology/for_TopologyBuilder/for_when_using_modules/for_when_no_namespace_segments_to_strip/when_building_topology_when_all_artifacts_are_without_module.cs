/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.Applications;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_no_namespace_segments_to_strip
{
    public class when_building_topology_when_all_artifacts_are_without_module : given.a_configuration_without_topology_with_valid_artifacts_without_module
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => topology_builder.Build());

        It should_throw_invalid_artifact = () => result.ShouldBeOfExactType(typeof(InvalidArtifact));
    }
}