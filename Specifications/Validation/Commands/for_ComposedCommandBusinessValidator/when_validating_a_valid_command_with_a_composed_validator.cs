using System.Collections.Generic;
using Dolittle.FluentValidation.Commands;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Specs.Commands.for_ComposedCommandBusinessValidator
{
    [Subject(typeof(ComposedCommandBusinessValidator<>))]
    public class when_validating_a_valid_command_with_a_composed_validator : given.a_composed_command_business_validator
    {
        static IEnumerable<ValidationResult> result;

        Because of = () => result = composed_validator.ValidateFor(valid_command);

        It should_be_valid = () => result.ShouldBeEmpty();
    }
}