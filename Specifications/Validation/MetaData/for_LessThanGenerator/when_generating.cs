/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Dolittle.FluentValidation.MetaData.for_LessThanGenerator
{
    public class when_generating
    {
        static LessThanValidator validator;
        static LessThanGenerator generator;
        static LessThan result;

        Establish context = () =>
        {
            validator = new LessThanValidator(5.7f);
            generator = new LessThanGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty", validator) as LessThan;

        It should_create_a_rule = () => result.ShouldNotBeNull();
        It should_pass_along_the_value = () => result.Value.ShouldEqual(validator.ValueToCompare);
    }
}
