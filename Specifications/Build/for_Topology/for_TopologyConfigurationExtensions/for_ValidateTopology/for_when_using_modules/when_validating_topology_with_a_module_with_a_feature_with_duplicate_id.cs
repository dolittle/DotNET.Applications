/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
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
            var feature = new FeatureDefinition(){Feature = id, Name = "Module"};
            var module = new ModuleDefinition(){Module = id, Name = "Module", Features = new []{feature}};
            topology = new Applications.Configuration.Topology() {Modules = new[]{module}};
        };

        Because of = () => exception_result = Catch.Exception(() => topology.ValidateTopology(true, logger));

        It should_be_an_invalid_topology = () => exception_result.ShouldNotBeNull();
        It should_throw_InvalidTopology = () => exception_result.ShouldBeOfExactType(typeof(InvalidTopology));
    }
}