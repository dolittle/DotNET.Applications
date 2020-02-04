// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Queries;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_not_using_modules.for_when_no_namespace_segments_to_strip.given
{
    public class valid_artifacts : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Type[] artifacts = new[]
        {
            typeof(Specs.Feature.TheCommand),
            typeof(Specs.Feature.TheEvent),
            typeof(Specs.Feature.Feature2.TheEvent),
            typeof(Specs.Feature3.TheQuery)
        };
    }
}