using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation
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
