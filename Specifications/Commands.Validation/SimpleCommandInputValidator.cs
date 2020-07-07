// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentValidation;

namespace Dolittle.Commands.Validation
{
    public class SimpleCommandInputValidator : CommandInputValidatorFor<SimpleCommand>
    {
        public SimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}