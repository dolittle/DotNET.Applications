/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyConfigurationExtensions.for_ValidateTopology.for_when_using_modules
{
    public class when_validating_topology_with_a_module_with_a_feature_with_duplicate_id : given.an_ILogger
    {

        static Applications.Configuration.Topology topology; 
        static Exception exception_result;

        Establish context = () => 
        {
            var id = Guid.NewGuid();
            var feature = new FeatureDefinition("Feature", new Dictionary<Feature, FeatureDefinition>());
            var module = new ModuleDefinition("Module", new Dictionary<Feature, FeatureDefinition>
            {
                {id, feature}
            });
            topology = new Applications.Configuration.Topology(new Dictionary<Module, ModuleDefinition>
            {
                {id, module}
            }, new Dictionary<Feature, FeatureDefinition>());
        };

        Because of = () => exception_result = Catch.Exception(() => topology.ValidateTopology(true, build_messages));

        It should_be_an_invalid_topology = () => exception_result.ShouldNotBeNull();
        It should_throw_InvalidTopology = () => exception_result.ShouldBeOfExactType(typeof(InvalidTopology));
    }
}