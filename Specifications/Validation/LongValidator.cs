using Dolittle.Concepts;
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Specs
{
    public class LongValidator : BusinessValidator<ConceptAs<long>>
    {
        public LongValidator()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
