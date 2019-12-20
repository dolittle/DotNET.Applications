// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_created : given.a_command_validator_provider_with_input_and_business_validators
    {
        It should_register_all_the_input_validators = () => command_validator_provider.RegisteredInputCommandValidators.ShouldContainOnly(command_input_validators);
        It should_register_all_the_business_validators = () => command_validator_provider.RegisteredBusinessCommandValidators.ShouldContainOnly(command_business_validators);
    }
}