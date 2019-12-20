// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Dolittle.FluentValidation.MetaData.for_RequiredGenerator
{
    public class when_generating_from_not_null
    {
        static NotNullValidator validator;
        static RequiredGenerator generator;
        static Required result;

        Establish context = () =>
        {
            validator = new NotNullValidator();
            generator = new RequiredGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty", validator) as Required;

        It should_create_a_rule = () => result.ShouldNotBeNull();
    }
}
