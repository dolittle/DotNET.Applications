// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidatorFor<SimpleCommand>
    {
        public SimpleCommandBusinessValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}