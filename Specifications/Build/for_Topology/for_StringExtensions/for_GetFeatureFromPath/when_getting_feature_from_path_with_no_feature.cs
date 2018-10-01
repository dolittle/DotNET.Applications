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
    public class when_getting_feature_from_path_with_no_feature
    {
        static readonly string empty_path = "";
        static readonly string path_with_only_whitespace = " ";
        static readonly string path_containing_whitespace = "Invalid Feature";
        static readonly string path_containing_dash = "Invalid-Feature";

        static Exception exception_result_for_empty_path;
        static Exception exception_result_for_path_with_only_whitespace;
        static Exception exception_result_for_path_containing_whitespace;
        static Exception exception_result_for_path_containing_dash;
        Because of = () => 
        {
            exception_result_for_empty_path = Catch.Exception(() => empty_path.GetFeatureFromPath());
            exception_result_for_path_with_only_whitespace = Catch.Exception(() => path_with_only_whitespace.GetFeatureFromPath());
            exception_result_for_path_containing_whitespace = Catch.Exception(() => path_containing_whitespace.GetFeatureFromPath());
            exception_result_for_path_containing_dash = Catch.Exception(() => path_containing_dash.GetFeatureFromPath());
        };

        It should_throw_exception_when_getting_feature_from_empty_path = () => exception_result_for_empty_path.ShouldNotBeNull();
        It should_throw_exception_when_getting_feature_from_path_with_only_whitespace = () => exception_result_for_path_with_only_whitespace.ShouldNotBeNull();
        It should_throw_exception_when_getting_feature_from_path_containing_whitespace = () => exception_result_for_path_containing_whitespace.ShouldNotBeNull();
        It should_throw_exception_when_getting_feature_from_path_containing_dash = () => exception_result_for_path_containing_dash.ShouldNotBeNull();

    }
}