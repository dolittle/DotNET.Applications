using FluentValidation;

namespace doLittle.FluentValidation.Specs.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : BusinessValidator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            RuleFor(c => c.Value).NotEmpty();
        }
    }
}
