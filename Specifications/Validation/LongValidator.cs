using doLittle.Concepts;
using FluentValidation;

namespace doLittle.FluentValidation.Specs
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
