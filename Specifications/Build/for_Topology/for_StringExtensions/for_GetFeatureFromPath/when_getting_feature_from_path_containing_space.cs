// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetFeatureFromPath
{
    public class when_getting_feature_from_path_containing_space
    {
        const string path_containing_whitespace = "Invalid Feature";
        static Exception exception_result_for_path_containing_whitespace;

        Because of_getting_feature_definition = () => exception_result_for_path_containing_whitespace = Catch.Exception(() => path_containing_whitespace.GetFeatureFromPath());

        It should_throw_an_exception = () => exception_result_for_path_containing_whitespace.ShouldNotBeNull();
        It should_throw_ArgumentException = () => exception_result_for_path_containing_whitespace.ShouldBeOfExactType<InvalidPathForFeature>();
    }
}