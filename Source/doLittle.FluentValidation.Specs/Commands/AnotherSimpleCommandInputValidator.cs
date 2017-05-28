using doLittle.FluentValidation.Commands;
using FluentValidation;

namespace doLittle.FluentValidation.Specs.Commands
{
    public class AnotherSimpleCommandInputValidator : CommandInputValidator<AnotherSimpleCommand>
    {
        public AnotherSimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}