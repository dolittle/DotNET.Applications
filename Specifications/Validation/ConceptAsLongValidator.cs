using FluentValidation;

namespace doLittle.FluentValidation.Specs
{
    public class ConceptAsLongValidator : BusinessValidator<ConceptAsLong>
    {
        public ConceptAsLongValidator()
        {
            ModelRule()
                .NotNull()
                .SetValidator(new LongValidator());
        }
    }
}
