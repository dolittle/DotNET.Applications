// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_an_input_validator_for_a_type_with_no_input_validator_for_the_second_time : given.a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate input_validator;
        static MySimpleCommand command;

        Establish context = () => command = new MySimpleCommand();

        Because of = () => input_validator = command_validator_provider.GetInputValidatorFor(command);

        It should_return_the_a_dynamically_constructed_validator = () => input_validator.ShouldBeOfExactType(typeof(ComposedCommandInputValidatorFor<MySimpleCommand>));
    }
}