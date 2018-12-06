/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_no_namespace_segments_to_strip.given
{
    public class a_configuration_without_topology_with_artifacts_with_module_where_one_artifact_does_not_match_topology : artifacts_with_module_where_one_artifact_does_not_match_topology
    {
        protected static readonly BoundedContextTopology configuration = new BoundedContextTopology(
            new Applications.Configuration.Topology(
                new ModuleDefinition[0],
                new FeatureDefinition[0]
            ), true, new Dictionary<Area, IEnumerable<string>>());

        protected static readonly TopologyBuilder topology_builder = new TopologyBuilder(artifacts, configuration, logger);
        
    }
}