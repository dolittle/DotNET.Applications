/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Commands.Validation;
using Dolittle.FluentValidation.Concepts.given;
using FluentValidation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_ComposedCommandBusinessValidator.given
{
    public class a_composed_command_business_validator : commands
    {
        protected static ComposedCommandBusinessValidatorFor<MySimpleCommand> composed_validator;

        protected static MySimpleCommand valid_command;
        protected static MySimpleCommand command_with_invalid_string;
        protected static MySimpleCommand command_with_invalid_long;
            
        Establish context = () =>
            {
                valid_command = new MySimpleCommand
                    {
                        LongConcept = 100,
                        StringConcept = "blah"
                    };

                command_with_invalid_string = new MySimpleCommand
                    {
                        LongConcept = 100
                    };

                command_with_invalid_long = new MySimpleCommand
                    {
                        StringConcept = "blah"
                    };

                var validators = new Dictionary<Type, IEnumerable<IValidator>>
                    {
                        {typeof (concepts.StringConcept), new[] {new StringConceptBusinessValidator()}},
                        {typeof (concepts.LongConcept), new[] {new LongConceptBusinessValidator()}}
                    };

                composed_validator = new ComposedCommandBusinessValidatorFor<MySimpleCommand>(validators);
            };
    }
}