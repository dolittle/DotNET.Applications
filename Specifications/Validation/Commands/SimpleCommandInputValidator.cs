using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Commands
{
    public class SimpleCommandInputValidator : CommandInputValidatorFor<SimpleCommand>
    {
        public SimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}