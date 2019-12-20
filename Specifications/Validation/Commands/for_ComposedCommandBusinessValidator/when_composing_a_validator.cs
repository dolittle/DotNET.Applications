// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_ComposedCommandBusinessValidator
{
    [Subject(typeof(ComposedCommandBusinessValidatorFor<>))]
    public class when_composing_a_validator : given.a_composed_command_business_validator
    {
        It should_create_rules_for_each_type_validator_combination_passed_in = () => composed_validator.ShouldNotBeNull();
    }
}