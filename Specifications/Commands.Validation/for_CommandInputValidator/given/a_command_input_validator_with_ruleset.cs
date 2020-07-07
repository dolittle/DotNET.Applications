// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandInputValidator.given
{
    public class a_command_input_validator_with_ruleset
    {
        protected static ICanValidate simple_command_input_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
        {
            simple_command_input_validator = new SimpleCommandInputValidatorWithRuleset();
            simple_command = new SimpleCommand();
        };
    }
}