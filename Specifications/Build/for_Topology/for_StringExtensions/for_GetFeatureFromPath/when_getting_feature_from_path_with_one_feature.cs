// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetFeatureFromPath
{
    public class when_getting_feature_from_path_with_one_feature
    {
        const string feature_name = "Feature1";
        static KeyValuePair<Feature, FeatureDefinition> feature_definition;

        Because of = () => feature_definition = feature_name.GetFeatureFromPath();

        It should_generate_a_feature_definition = () => feature_definition.ShouldNotBeNull();
        It should_should_have_first_feature_definition_with_correct_name = () => feature_definition.Value.Name.ShouldEqual((FeatureName)feature_name);
        It should_should_have_first_feature_definition_with_valid_feature_id = () => feature_definition.Key.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_first_feature_definition_with_no_sub_features = () => feature_definition.Value.SubFeatures.Count.ShouldEqual(0);
    }
}