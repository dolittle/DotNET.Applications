// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetFeatureFromPath
{
    public class when_getting_feature_from_path_with_three_features
    {
        const string feature1_name = "Feature1";
        const string feature2_name = "Feature2";
        const string feature3_name = "Feature3";
        static readonly string feature_path = $"{feature1_name}.{feature2_name}.{feature3_name}";
        static KeyValuePair<Feature, FeatureDefinition> feature_definition;

        Because of = () => feature_definition = feature_path.GetFeatureFromPath();

        It should_generate_a_feature_definition = () => feature_definition.ShouldNotBeNull();
        It should_should_have_first_feature_definition_with_correct_name = () => feature_definition.Value.Name.ShouldEqual((FeatureName)feature1_name);
        It should_should_have_first_feature_definition_with_valid_feature_id = () => feature_definition.Key.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_first_feature_definition_with_one_sub_feature = () => feature_definition.Value.SubFeatures.Count.ShouldEqual(1);

        It should_should_have_second_feature_definition_with_correct_name = () => feature_definition.Value.SubFeatures.First().Value.Name.ShouldEqual((FeatureName)feature2_name);
        It should_should_have_second_feature_definition_with_valid_feature_id = () => feature_definition.Value.SubFeatures.First().Key.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_second_feature_definition_with_one_sub_features = () => feature_definition.Value.SubFeatures.First().Value.SubFeatures.Count.ShouldEqual(1);

        It should_should_have_third_feature_definition_with_correct_name = () => feature_definition.Value.SubFeatures.First().Value.SubFeatures.First().Value.Name.ShouldEqual((FeatureName)feature3_name);
        It should_should_have_third_feature_definition_with_valid_feature_id = () => feature_definition.Value.SubFeatures.First().Value.SubFeatures.First().Key.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_third_feature_definition_with_no_sub_features = () => feature_definition.Value.SubFeatures.First().Value.SubFeatures.First().Value.SubFeatures.Count.ShouldEqual(0);
    }
}