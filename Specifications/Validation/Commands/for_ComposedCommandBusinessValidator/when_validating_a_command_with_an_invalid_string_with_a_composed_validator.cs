// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_ComposedCommandBusinessValidator
{
    [Subject(typeof(ComposedCommandBusinessValidatorFor<>))]
    public class when_validating_a_command_with_an_invalid_string_with_a_composed_validator : given.a_composed_command_business_validator
    {
        static IEnumerable<ValidationResult> result;

        Because of = () => result = composed_validator.ValidateFor(command_with_invalid_string);

        It should_not_be_valid = () => result.ShouldNotBeEmpty();
    }
}