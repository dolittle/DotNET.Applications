// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.ApplicationModel;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_TopologyBuilder.for_when_using_modules.for_when_no_namespace_segments_to_strip
{
    public class when_building_topology_when_all_artifacts_are_with_module : given.a_configuration_without_topology_with_valid_artifacts_with_module
    {
        static Applications.Configuration.Topology topology;

        Because of = () => topology = topology_builder.Build();

        It should_generate_a_topology = () => topology.ShouldNotBeNull();
        It should_generate_a_topology_with_modules = () => topology.Modules.ShouldNotBeEmpty();
        It should_generate_a_topology_with_no_features = () => topology.Features.ShouldBeEmpty();
        It should_generate_a_topology_with_one_module = () => topology.Modules.Count.ShouldEqual(1);
        It should_generate_a_topology_with_two_top_level_features = () => topology.Modules.First().Value.Features.Count.ShouldEqual(2);
        It should_have_a_single_top_level_feature_where_name_is_Feature = () => topology.Modules.First().Value.Features.Single(_ => _.Value.Name == (FeatureName)"Feature").ShouldNotBeNull();
        It should_have_a_single_top_level_feature_where_name_is_Feature3 = () => topology.Modules.First().Value.Features.Single(_ => _.Value.Name == (FeatureName)"Feature3").ShouldNotBeNull();
        It should_have_a_single_top_level_feature_that_has_sub_features = () => topology.Modules.First().Value.Features.Single(_ => _.Value.SubFeatures.Count > 0).ShouldNotBeNull();
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature = () => topology.Modules.First().Value.Features.Single(_ => _.Value.Name == (FeatureName)"Feature").Value.SubFeatures.Count.ShouldEqual(1);
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature_with_name_Feature3 = () => topology.Modules.First().Value.Features.Single(_ => _.Value.Name == (FeatureName)"Feature").Value.SubFeatures.Single(_ => _.Value.Name == (FeatureName)"Feature2").ShouldNotBeNull();
        It should_have_a_single_feature_with_name_Feature_that_has_a_single_sub_feature_with_name_Feature3_that_has_no_sub_features = () => topology.Modules.First().Value.Features.Single(_ => _.Value.Name == (FeatureName)"Feature").Value.SubFeatures.Single(_ => _.Value.Name == (FeatureName)"Feature2").Value.SubFeatures.ShouldBeEmpty();
    }
}