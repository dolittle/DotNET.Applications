// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandInputValidator
{
    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_not_in_the_ruleset_and_ruleset_is_not_specified : given.a_command_input_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = 10;
        };

        Because of = () => results = simple_command_input_validator.ValidateFor(simple_command);

        It should_have_an_invalid_property = () => results.Count().ShouldEqual(1);
    }
}