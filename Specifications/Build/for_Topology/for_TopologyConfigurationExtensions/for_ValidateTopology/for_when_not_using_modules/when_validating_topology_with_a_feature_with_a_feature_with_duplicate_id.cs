/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyConfigurationExtensions.for_ValidateTopology.for_when_not_using_modules
{
    public class when_validating_topology_with_a_feature_with_a_feature_with_duplicate_id : given.an_ILogger
    {
        static Applications.Configuration.Topology topology; 
        static Exception exception_result;

        Establish context = () => 
        {
            var feature_id = Guid.NewGuid();
            var feature_def_2 = new FeatureDefinition("Feature2", new Dictionary<Feature,FeatureDefinition>());
            var feature_def_1 = new FeatureDefinition("Feature1", new Dictionary<Feature, FeatureDefinition>
            {
                {feature_id, feature_def_2}
            });

            topology = new Applications.Configuration.Topology(new Dictionary<Module,ModuleDefinition>(), new Dictionary<Feature, FeatureDefinition>
            {
                {feature_id, feature_def_1}
            });
        };
        Because of = () => exception_result = Catch.Exception(() => topology.ValidateTopology(false, logger));

        It should_be_an_invalid_topology = () => exception_result.ShouldNotBeNull();
        It should_throw_InvalidTopology = () => exception_result.ShouldBeOfExactType(typeof(InvalidTopology));
    }
}