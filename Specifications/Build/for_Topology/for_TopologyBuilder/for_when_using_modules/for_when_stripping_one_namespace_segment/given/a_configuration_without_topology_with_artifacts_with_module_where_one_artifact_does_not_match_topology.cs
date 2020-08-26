// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_stripping_one_namespace_segment.given
{
    public class a_configuration_without_topology_with_artifacts_with_module_where_one_artifact_does_not_match_topology : artifacts_with_module_where_one_artifact_does_not_match_topology
    {
        protected static readonly MicroserviceTopology configuration = new MicroserviceTopology(
            new Applications.Configuration.Topology(
                new Dictionary<Module, ModuleDefinition>(),
                new Dictionary<Feature, FeatureDefinition>()),
            true,
            new Dictionary<Area, IEnumerable<string>>());

        protected static readonly TopologyBuilder topology_builder = new TopologyBuilder(artifacts, configuration, build_messages);

        Establish context = () => configuration.NamespaceSegmentsToStrip.Add("Specs", new[] { "ToStrip" });
    }
}