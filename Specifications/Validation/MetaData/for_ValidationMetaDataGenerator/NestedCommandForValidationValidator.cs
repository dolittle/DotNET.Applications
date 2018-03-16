using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedCommandForValidationValidator : CommandInputValidatorFor<NestedCommandForValidation>
    {
        public NestedCommandForValidationValidator(CommandForValidationValidator commandForValidationValidator)
        {
            RuleFor(n => n.SomeCommand).SetValidator(commandForValidationValidator);
            RuleFor(n => n.FirstLevelString).NotEmpty().WithMessage("OMG WHY ARE YOU EMPTY??!?! NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!");
        }
    }
}