using FluentValidation;

namespace Dolittle.FluentValidation.Specs
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
