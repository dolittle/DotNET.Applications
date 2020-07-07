// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_a_business_validator_for_something_that_is_not_a_command : given.a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate business_validator;

        Because of = () => business_validator = command_validator_provider.GetBusinessValidatorFor(typeof(string));

        It should_return_null = () => business_validator.ShouldBeNull();
    }
}