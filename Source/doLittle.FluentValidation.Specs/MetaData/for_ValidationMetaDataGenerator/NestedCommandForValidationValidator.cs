using doLittle.FluentValidation.Commands;
using FluentValidation;

namespace doLittle.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedCommandForValidationValidator : CommandInputValidator<NestedCommandForValidation>
    {
        public NestedCommandForValidationValidator(CommandForValidationValidator commandForValidationValidator)
        {
            RuleFor(n => n.SomeCommand).SetValidator(commandForValidationValidator);
            RuleFor(n => n.FirstLevelString).NotEmpty().WithMessage("OMG WHY ARE YOU EMPTY??!?! NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!");
        }
    }
}