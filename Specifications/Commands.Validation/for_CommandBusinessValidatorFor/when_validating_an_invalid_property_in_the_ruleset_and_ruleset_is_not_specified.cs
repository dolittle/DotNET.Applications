// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.Commands.Validation.for_CommandBusinessValidatorFor
{
    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_in_the_ruleset_and_ruleset_is_not_specified : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = "And to him only shall be given...";
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command);

        It should_not_have_invalid_properties = () => results.Any().ShouldBeFalse();
    }
}