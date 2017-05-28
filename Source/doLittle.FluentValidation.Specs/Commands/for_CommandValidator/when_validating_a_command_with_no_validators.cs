using System.Dynamic;
using doLittle.Applications;
using doLittle.Commands;
using doLittle.FluentValidation.Commands;
using doLittle.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.FluentValidation.Specs.Commands.for_CommandValidator
{
    public class when_validating_a_command_with_no_validators : given.a_command_validation_service
    {
        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;

        Establish context = () =>
                                {
                                    command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
                                    command_instance = Mock.Of<ICommand>();
                                    command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

                                    command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_instance)).Returns(() => new NullCommandInputValidator<ICommand>());
                                    command_validator_provider_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_instance)).Returns(() => new NullCommandBusinessValidator<ICommand>());
                                };

        Because of = () => result = command_validator.Validate(command);

        It should_have_no_failed_validation_results = () => result.ValidationResults.ShouldBeEmpty();
    }
}
