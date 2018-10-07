/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactConfigurationBuilder.when_not_using_modules.given
{
    public class a_bounded_context_config : all_dependencies
    {
        protected static BoundedContextTopology bounded_context_config;
        Establish context = () => 
        {
            var topology = new Applications.Configuration.Topology(){Features = new []
            {
                new FeatureDefinition(){Feature = first_feature, Name = "Feature"},
                new FeatureDefinition(){Feature = second_feature, Name = "Feature3"}
            }};
            
            bounded_context_config = new BoundedContextTopology(topology, false, new Dictionary<Area, IEnumerable<string>>());
        };
    }
}