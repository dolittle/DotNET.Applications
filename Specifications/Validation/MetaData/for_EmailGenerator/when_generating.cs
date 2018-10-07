/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Dolittle.FluentValidation.MetaData.for_EmailGenerator
{
    public class when_generating
    {
        static EmailValidator validator;
        static EmailGenerator generator;
        static Email result;

        Establish context = () =>
        {
            validator = new EmailValidator();
            generator = new EmailGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty", validator) as Email;

        It should_create_a_rule = () => result.ShouldNotBeNull();
    }
}
