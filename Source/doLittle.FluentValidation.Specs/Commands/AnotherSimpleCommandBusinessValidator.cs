using System;
using System.Collections.Generic;
using doLittle.FluentValidation.Commands;
using doLittle.Validation;

namespace doLittle.FluentValidation.Specs.Commands
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidator<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(AnotherSimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}