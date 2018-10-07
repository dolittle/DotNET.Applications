/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Topology.for_Topology.for_StringExtensions.for_GetModuleFromPath
{
    public class when_getting_module_from_path_with_module_and_no_feature : given.a_module_name
    {
         static readonly string path = $"{module_name}";

        static ModuleDefinition module_definition;

        Because of = () => module_definition = path.GetModuleFromPath();

        It should_get_a_module_definition = () => module_definition.ShouldNotBeNull();
        It should_have_the_correct_module_name = () => module_definition.Name.ShouldEqual((ModuleName)module_name);
        It should_have_a_valid_module_id = () => module_definition.Module.ShouldNotEqual((Module)Guid.Empty);
        It should_have_one_feature = () => module_definition.Features.Count().ShouldEqual(0);
    }
}