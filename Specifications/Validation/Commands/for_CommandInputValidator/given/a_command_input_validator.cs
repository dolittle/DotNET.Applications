// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
}