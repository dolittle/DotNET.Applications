using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidatorFor<SimpleCommand>
    {
        public SimpleCommandBusinessValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}