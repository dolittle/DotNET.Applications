using Dolittle.Runtime.Commands;
using Dolittle.FluentValidation.Commands;
using Dolittle.FluentValidation.Specs.Commands.for_CommandValidatorProvider.given;
using Machine.Specifications;
using Moq;

namespace Dolittle.FluentValidation.Specs.Commands.for_CommandValidator.given
{
    public class a_command_validation_service : a_command_validator_provider_with_input_and_business_validators
    {
        protected static ICommandValidator command_validator;
        protected static Mock<ICommandValidatorProvider> command_validator_provider_mock;
        protected static Mock<ICommandRequestConverter> command_request_converter;

        Establish context = () =>
                                {
                                    command_validator_provider_mock = new Mock<ICommandValidatorProvider>();
                                    command_request_converter = new Mock<ICommandRequestConverter>();
                                    command_validator = new CommandValidator(command_validator_provider_mock.Object, command_request_converter.Object);
                                };
    }
}