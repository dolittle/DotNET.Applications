using System.Dynamic;
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.Commands.Validation;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Runtime.Commands.Validation;

namespace Dolittle.FluentValidation.Specs.Commands.for_CommandValidator
{
    public class when_validating_a_command_with_no_validators : given.a_command_validation_service
    {
        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;

        Establish context = () =>
                                {
                                    command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationArtifactIdentifier>(), new ExpandoObject());
                                    command_instance = Mock.Of<ICommand>();
                                    command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);

                                    command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_instance)).Returns(() => new NullCommandInputValidatorFor<ICommand>());
                                    command_validator_provider_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_instance)).Returns(() => new NullCommandBusinessValidatorFor<ICommand>());
                                };

        Because of = () => result = command_validator.Validate(command);

        It should_have_no_failed_validation_results = () => result.ValidationResults.ShouldBeEmpty();
    }
}
