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

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_not_using_modules.given
{
    public class a_bounded_context_config : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Feature feature1 = Guid.NewGuid();
        protected static readonly Feature feature2 = Guid.NewGuid();
        protected static BoundedContextTopology bounded_context_config;
        Establish context = () => 
        {
            var topology = new Applications.Configuration.Topology(){Features = new []
            {
                new FeatureDefinition(){Feature = feature1, Name = "Feature"},
                new FeatureDefinition(){Feature = feature2, Name = "Feature3"}
            }};
            
            bounded_context_config = new BoundedContextTopology(topology,false, new Dictionary<Area, IEnumerable<string>>());
        };
    }
}