/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Dynamic;
using System.Linq;
using Dolittle.Commands;
using Dolittle.Commands.Validation;
using Dolittle.Runtime.Commands;
using Dolittle.Execution;
using Dolittle.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Artifacts;
using Dolittle.Runtime.Commands.Validation;

namespace Dolittle.FluentValidation.Commands.for_CommandValidator
{
    public class when_validating_a_command_that_has_model_rule_and_property_errors_on_input_validator : given.a_command_validation_service
    {
        const string ErrorMessage = "Something went wrong";
        const string AnotherErrorMessage = "Something else went wrong";

        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;
        static Mock<ICommandInputValidator> command_input_validator;

        Establish context = () =>
        {
            var artifact = Artifact.New();
            command = new CommandRequest(CorrelationId.Empty, artifact.Id, artifact.Generation, new ExpandoObject());
            command_instance = Mock.Of<ICommand>();
            command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

            command_input_validator = new Mock<ICommandInputValidator>();
            command_input_validator.Setup(c => c.ValidateFor(command_instance)).Returns(new []
            {
                new ValidationResult(ErrorMessage, new [] { ModelRule<object>.ModelRulePropertyName }),
                    new ValidationResult(AnotherErrorMessage, new [] { "SomeProperty" })
            });

            command_validator_provider_mock.Setup(c => c.GetInputValidatorFor(command_instance)).Returns(command_input_validator.Object);
        };

        Because of = () => result = command_validator.Validate(command);

        It should_have_one_command_error_message = () => result.CommandErrorMessages.Count().ShouldEqual(1);
        It should_have_the_correct_command_error_message = () => result.CommandErrorMessages.First().ShouldEqual(ErrorMessage);
        It should_have_one_validation_result = () => result.ValidationResults.Count().ShouldEqual(1);
        It should_have_the_correct_validation_result = () => result.ValidationResults.First().ErrorMessage.ShouldEqual(AnotherErrorMessage);
    }
}