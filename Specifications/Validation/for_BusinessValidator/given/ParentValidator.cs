// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.for_BusinessValidator.given
{
    public class ParentValidator : BusinessValidator<Parent>
    {
        public ParentValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(p => p.SimpleStringProperty)
                .NotEmpty();
            RuleFor(p => p.SimpleIntegerProperty)
                .LessThan(10);
            RuleFor(p => p.Child)
                .SetValidator(new ChildValidator());
        }
    }
}