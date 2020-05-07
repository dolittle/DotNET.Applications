// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_not_using_modules.for_when_no_namespace_segments_to_strip.given
{
    public class artifacts_where_one_artifact_does_not_match_topology : Build.given.an_ILogger
    {
        protected static readonly Type[] artifacts = new[]
        {
            typeof(Specs.Feature.TheCommand),
            typeof(Specs.Feature.TheEvent),
            typeof(Specs.Feature.Feature2.TheEvent),
            typeof(Specs.Feature3.TheQuery),
            typeof(Specs.InvalidEvent)
        };
    }
}