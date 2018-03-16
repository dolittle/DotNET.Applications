using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Specs.Commands.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_an_input_validator_for_something_that_is_not_a_command : given.a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate input_validator;

        Because of = () => input_validator = command_validator_provider.GetInputValidatorFor(typeof(string));

        It should_return_null = () => input_validator.ShouldBeNull();
    }
}