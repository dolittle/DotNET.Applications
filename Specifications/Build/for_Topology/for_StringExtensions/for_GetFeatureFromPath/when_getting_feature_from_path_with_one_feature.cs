/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetFeatureFromPath
{
    public class when_getting_feature_from_path_with_one_feature
    {
        static readonly string feature1_name = "Feature1";
        static readonly string feature_path = $"{feature1_name}";

        static FeatureDefinition feature_definition;
        Because of = () => 
        {
            feature_definition = feature_path.GetFeatureFromPath();
            
        };

        It should_generate_a_FeatureDefinition = () => feature_definition.ShouldNotBeNull();
        It should_should_have_first_FeatureDefinition_with_correct_name = () => feature_definition.Name.ShouldEqual((FeatureName)feature1_name);
        It should_should_have_first_FeatureDefinition_with_valid_feature_id = () => feature_definition.Feature.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_first_FeatureDefinition_with_no_sub_features = () => feature_definition.SubFeatures.Count().ShouldEqual(0);

    }
}