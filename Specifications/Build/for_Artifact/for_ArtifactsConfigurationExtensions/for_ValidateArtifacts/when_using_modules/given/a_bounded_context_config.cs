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

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_using_modules.given
{
    public class a_bounded_context_config : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Module module = Guid.NewGuid();
        protected static readonly Feature feature1 = Guid.NewGuid();
        protected static readonly Feature feature2 = Guid.NewGuid();
        protected static BoundedContextTopology bounded_context_config;
        Establish context = () => 
        {
            var modules = new []
            {
                new ModuleDefinition(){Module = Guid.NewGuid(), Name = "Module", Features = new []
                {
                    new FeatureDefinition(){Feature = feature1, Name = "Feature"},
                    new FeatureDefinition(){Feature = feature2, Name = "Feature3"}
                }}
            };
            var topology = new Applications.Configuration.Topology(){Modules = modules};
            
            bounded_context_config = new BoundedContextTopology(topology, true, new Dictionary<Area, IEnumerable<string>>());
        };
    }
}