using System.Collections.Generic;
using System.Dynamic;
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.FluentValidation.Commands;
using Dolittle.Validation;
using Dolittle.Runtime.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.FluentValidation.Specs.Commands.for_CommandValidator
{
    public class when_validating_a_command_that_passes : given.a_command_validation_service
    {
        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;
        static Mock<ICommandInputValidator> command_input_validator;
        static Mock<ICommandBusinessValidator> command_business_validator;

        Establish context = () =>
        {
            command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
            command_instance = Mock.Of<ICommand>();
            command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

            command_input_validator = new Mock<ICommandInputValidator>();
            command_business_validator = new Mock<ICommandBusinessValidator>();

            command_input_validator.Setup(iv => iv.ValidateFor(command_instance)).Returns(new List<ValidationResult>());
            command_business_validator.Setup(cv => cv.ValidateFor(command_instance)).Returns(new List<ValidationResult>());

            command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_instance)).Returns(command_input_validator.Object);
            command_validator_provider_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_instance)).Returns(command_business_validator.Object);
        };

        Because of = () => result = command_validator.Validate(command);

        It should_have_no_failed_validation_results = () => result.ValidationResults.ShouldBeEmpty();
        It should_have_validated_the_command_inputs = () => command_input_validator.VerifyAll();
        It should_have_validated_the_command_business_rules = () => command_business_validator.VerifyAll();
    }
}