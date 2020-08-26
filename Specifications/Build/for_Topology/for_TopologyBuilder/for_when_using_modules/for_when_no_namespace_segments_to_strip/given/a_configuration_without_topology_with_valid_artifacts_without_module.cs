// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_no_namespace_segments_to_strip.given
{
    public class a_configuration_without_topology_with_valid_artifacts_without_module : valid_artifacts_without_module
    {
        protected static readonly MicroserviceTopology configuration = new MicroserviceTopology(
            new Applications.Configuration.Topology(
                new Dictionary<Module, ModuleDefinition>(),
                new Dictionary<Feature, FeatureDefinition>()),
            true,
            new Dictionary<Area, IEnumerable<string>>());

        protected static readonly TopologyBuilder topology_builder = new TopologyBuilder(artifacts, configuration, build_messages);
    }
}