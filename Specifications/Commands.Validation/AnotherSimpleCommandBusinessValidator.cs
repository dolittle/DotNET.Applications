// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Validation;

namespace Dolittle.Commands.Validation
{
    public class AnotherSimpleCommandBusinessValidator : CommandBusinessValidatorFor<AnotherSimpleCommand>
    {
        public override IEnumerable<ValidationResult> ValidateFor(AnotherSimpleCommand instance)
        {
#pragma warning disable DL0008
            throw new NotImplementedException();
#pragma warning restore DL0008
        }
    }
}