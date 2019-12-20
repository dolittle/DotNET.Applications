// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Commands.Validation;
using Dolittle.FluentValidation.Concepts.given;
using FluentValidation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_ComposedCommandInputValidator.given
{
    public class a_composed_command_input_validator
    {
        protected static ComposedCommandInputValidatorFor<MySimpleCommand> composed_validator;
        protected static MySimpleCommand valid_command;
        protected static MySimpleCommand command_with_invalid_string;
        protected static MySimpleCommand command_with_invalid_long;

        Establish context = () =>
        {
            valid_command = new MySimpleCommand
            {
                LongConcept = 100,
                StringConcept = "valid"
            };

            command_with_invalid_string = new MySimpleCommand
            {
                LongConcept = 100
            };

            command_with_invalid_long = new MySimpleCommand
            {
                StringConcept = "valid"
            };

            var validators = new Dictionary<Type, IEnumerable<IValidator>>
                {
                    { typeof(StringConcept), new[] { new StringConceptInputValidator() } },
                    { typeof(LongConcept), new[] { new LongConceptInputValidator() } }
                };

            composed_validator = new ComposedCommandInputValidatorFor<MySimpleCommand>(validators);
        };
    }
}