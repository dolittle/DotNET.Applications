// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetFeatureFromPath
{
    public class when_getting_feature_from_path_with_no_feature
    {
        const string empty_path = "";
        static Exception exception_result_for_empty_path;

        Because of_getting_feature_defintion = () => exception_result_for_empty_path = Catch.Exception(() => empty_path.GetFeatureFromPath());

        It should_throw_an_exception = () => exception_result_for_empty_path.ShouldNotBeNull();
        It should_throw_ArgumentException = () => exception_result_for_empty_path.ShouldBeOfExactType<InvalidPathForFeature>();
    }
}