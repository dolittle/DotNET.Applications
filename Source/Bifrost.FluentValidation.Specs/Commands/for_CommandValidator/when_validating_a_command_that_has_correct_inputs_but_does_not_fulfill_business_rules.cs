using System.Collections.Generic;
using System.Dynamic;
using Bifrost.Applications;
using Bifrost.Commands;
using Bifrost.FluentValidation.Commands;
using Bifrost.Lifecycle;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandValidator
{
    public class when_validating_a_command_that_has_correct_inputs_but_does_not_fulfill_business_rules : given.a_command_validation_service
    {
        static IEnumerable<ValidationResult> business_validation_errors;
        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;
        static Mock<ICommandInputValidator> command_input_validator;
        static Mock<ICommandBusinessValidator> command_business_validator;

        Establish context = () =>
        {
            business_validation_errors = new List<ValidationResult>()
                                          {
                                              new ValidationResult("first failed input", new[] { "AProperty" }),
                                              new ValidationResult("second failed input", new[] { "AnotherProperty" })
                                          };

            command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
            command_instance = Mock.Of<ICommand>();
            command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

            command_input_validator = new Mock<ICommandInputValidator>();
            command_business_validator = new Mock<ICommandBusinessValidator>();

            command_input_validator.Setup(iv => iv.ValidateFor(command)).Returns(new List<ValidationResult>());
            command_business_validator.Setup(cv => cv.ValidateFor(command)).Returns(business_validation_errors);

            command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_instance)).Returns(command_input_validator.Object);
            command_validator_provider_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_instance)).Returns(command_business_validator.Object);
        };

        Because of = () => result = command_validator.Validate(command);

        It should_have_failed_validations = () => result.ValidationResults.ShouldNotBeEmpty();
        It should_have_all_the_failed_business_rule_validations = () => result.ValidationResults.ShouldContainOnly(business_validation_errors);
        It should_have_validated_the_command_inputs = () => command_input_validator.VerifyAll();
    }
}