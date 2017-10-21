using FluentValidation;

namespace doLittle.FluentValidation.Specs
{
    public class ObjectValidator : BusinessValidator<object>
    {
        public ObjectValidator()
        {
            ModelRule()
                .NotNull();
        }
    }
}
