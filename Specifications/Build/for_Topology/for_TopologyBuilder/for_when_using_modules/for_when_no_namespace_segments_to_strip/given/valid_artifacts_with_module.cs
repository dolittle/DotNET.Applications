/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Queries;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_no_namespace_segments_to_strip.given
{
    public class valid_artifacts_with_module : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Type[] artifacts = new []
        {
            typeof(Specs.Module.Feature.TheCommand),
            typeof(Specs.Module.Feature.TheEvent),
            typeof(Specs.Module.Feature.Feature2.TheEvent),
            typeof(Specs.Module.Feature3.TheQuery)
        };
    }
}