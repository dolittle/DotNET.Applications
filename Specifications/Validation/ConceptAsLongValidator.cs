using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation
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
