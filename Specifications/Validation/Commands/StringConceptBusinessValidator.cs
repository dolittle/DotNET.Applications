// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.FluentValidation.Concepts.given;
using Dolittle.Validation;

namespace Dolittle.FluentValidation.Commands
{
    public class StringConceptBusinessValidator : BusinessValidator<StringConcept>
    {
        public StringConceptBusinessValidator()
        {
            RuleFor(s => s.Value)
                .Equals("Blah");
        }
    }
}