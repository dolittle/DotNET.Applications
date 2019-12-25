// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_CommandBusinessValidator
{
    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_command : given.a_command_business_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command);

        It should_have_invalid_properties = () => results.Count().ShouldEqual(2);
    }
}
