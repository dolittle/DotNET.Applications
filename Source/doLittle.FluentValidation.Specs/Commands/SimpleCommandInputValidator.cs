using doLittle.FluentValidation.Commands;
using FluentValidation;

namespace doLittle.FluentValidation.Specs.Commands
{
    public class SimpleCommandInputValidator : CommandInputValidator<SimpleCommand>
    {
        public SimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}