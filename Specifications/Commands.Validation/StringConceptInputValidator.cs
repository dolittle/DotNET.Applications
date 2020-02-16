// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    public class StringConceptInputValidator : InputValidator<StringConcept>
    {
        public StringConceptInputValidator()
        {
            RuleFor(s => s.Value)
                .NotEmpty();
        }
    }
}