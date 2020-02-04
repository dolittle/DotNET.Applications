// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Heads.for_RuntimeServiceDefinition
{
    public class when_creating_instance_with_type_not_inheriting_client_base
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => new RuntimeServiceDefinition(typeof(object), null));

        It should_throw_must_inherit_from_client_base = () => result.ShouldBeOfExactType<MustInheritFromClientBase>();
    }
}