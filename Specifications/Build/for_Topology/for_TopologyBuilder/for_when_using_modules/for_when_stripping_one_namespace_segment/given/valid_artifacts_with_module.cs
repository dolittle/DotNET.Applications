// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_stripping_one_namespace_segment.given
{
    public class valid_artifacts_with_module : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Type[] artifacts = new[]
        {
            typeof(Specs.ToStrip.Module.Feature.TheCommand),
            typeof(Specs.ToStrip.Module.Feature.TheEvent),
            typeof(Specs.ToStrip.Module.Feature.Feature2.TheEvent),
            typeof(Specs.ToStrip.Module.Feature3.TheQuery)
        };
    }
}