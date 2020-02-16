// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandBusinessValidatorFor.given
{
    public class a_command_business_validator
    {
        protected static ICanValidate simple_command_business_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
        {
            simple_command_business_validator = new SimpleCommandBusinessValidator();
            simple_command = new SimpleCommand();
        };
    }
}