using doLittle.FluentValidation.Commands;
using FluentValidation;

namespace doLittle.FluentValidation.Specs.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidator<SimpleCommand>
    {
        public SimpleCommandBusinessValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}