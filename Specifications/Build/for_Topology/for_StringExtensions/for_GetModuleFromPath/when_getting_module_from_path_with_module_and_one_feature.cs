// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.ApplicationModel;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetModuleFromPath
{
    public class when_getting_module_from_path_with_module_and_one_feature : given.a_module_name
    {
        const string feature1_name = "Feature1";
        static readonly string path = $"{module_name}.{feature1_name}";

        static KeyValuePair<Module, ModuleDefinition> module_definition;

        Because of = () => module_definition = path.GetModuleFromPath();

        It should_get_a_module_definition = () => module_definition.ShouldNotBeNull();
        It should_have_the_correct_module_name = () => module_definition.Value.Name.ShouldEqual((ModuleName)module_name);
        It should_have_a_valid_module_id = () => module_definition.Key.ShouldNotEqual((Module)Guid.Empty);
        It should_have_one_feature = () => module_definition.Value.Features.Count.ShouldEqual(1);

        It should_should_have_first_feature_definition_with_correct_name = () => module_definition.Value.Features.First().Value.Name.ShouldEqual((FeatureName)feature1_name);
        It should_should_have_first_feature_definition_with_valid_feature_id = () => module_definition.Value.Features.First().Key.ShouldNotEqual((Feature)Guid.Empty);
        It should_should_have_first_feature_definition_with_no_sub_features = () => module_definition.Value.Features.First().Value.SubFeatures.ShouldBeEmpty();
    }
}