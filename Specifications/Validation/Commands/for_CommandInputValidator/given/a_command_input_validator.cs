/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_CommandInputValidator.given
{
    public class a_command_input_validator
    {
        protected static SimpleCommandInputValidator simple_command_input_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
                                {
                                    simple_command_input_validator = new SimpleCommandInputValidator();
                                    simple_command = new SimpleCommand();
                                };
    }

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