using Dolittle.FluentValidation.Commands;
using FluentValidation;

namespace Dolittle.FluentValidation.Specs.Commands
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