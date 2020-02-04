// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetModuleFromPath
{
    public class when_getting_module_from_path_with_no_module
    {
         const string path = "";

         static Exception result_exception;

         Because of = () => result_exception = Catch.Exception(() => path.GetModuleFromPath());

         It should_throw_an_exception = () => result_exception.ShouldNotBeNull();
         It should_throw_ArgumentException = () => result_exception.ShouldBeOfExactType<InvalidPathForModule>();
    }
}