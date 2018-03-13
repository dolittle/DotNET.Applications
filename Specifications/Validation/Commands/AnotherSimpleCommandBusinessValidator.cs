using System;
using System.Collections.Generic;
using Dolittle.FluentValidation.Commands;
using Dolittle.Validation;

namespace Dolittle.FluentValidation.Specs.Commands
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidator<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(AnotherSimpleCommand instance)
        {
            throw new NotImplementedException();
        }
    }
}