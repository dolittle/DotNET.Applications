using System;
using System.Collections.Generic;
using Dolittle.Commands.Validation;
using Dolittle.Validation;

namespace Dolittle.FluentValidation.Commands
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidatorFor<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(AnotherSimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}