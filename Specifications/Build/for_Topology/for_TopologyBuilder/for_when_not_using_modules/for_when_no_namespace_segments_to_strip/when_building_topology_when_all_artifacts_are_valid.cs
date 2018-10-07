/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using Dolittle.Applications;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_not_using_modules.for_when_no_namespace_segments_to_strip
{
    public class when_building_topology_when_all_artifacts_are_valid : given.a_configuration_without_topology_with_valid_artifacts
    {
        static Applications.Configuration.Topology topology;

        Because of = () => topology = topology_builder.Build();

        It should_generate_a_topology = () => topology.ShouldNotBeNull();
        It should_generate_a_topology_with_features = () => topology.Features.ShouldNotBeEmpty();
        It should_generate_a_topology_with_no_modules = () => topology.Modules.ShouldBeEmpty();
        It should_generate_a_topology_with_two_top_level_features = () => topology.Features.Count().ShouldEqual(2);
        It should_have_a_single_top_level_feature_where_name_is_Feature = () => topology.Features.Single(_ => _.Name == (FeatureName)"Feature").ShouldNotBeNull();
        It should_have_a_single_top_level_feature_where_name_is_Feature3 = () => topology.Features.Single(_ => _.Name == (FeatureName)"Feature3").ShouldNotBeNull();
        It should_have_a_single_top_level_feature_that_has_sub_features = () => topology.Features.Single(_ => _.SubFeatures.Count() > 0).ShouldNotBeNull();
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature = () => topology.Features.Single(_ => _.Name == (FeatureName)"Feature").SubFeatures.Count().ShouldEqual(1);
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature_with_name_Feature3 = () => topology.Features.Single(_ => _.Name == (FeatureName)"Feature").SubFeatures.Single(_ => _.Name == (FeatureName)"Feature2").ShouldNotBeNull();
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature_with_name_Feature3_that_has_no_sub_features = () => topology.Features.Single(_ => _.Name == (FeatureName)"Feature").SubFeatures.Single(_ => _.Name == (FeatureName)"Feature2").SubFeatures.ShouldBeEmpty();
        
    }
}