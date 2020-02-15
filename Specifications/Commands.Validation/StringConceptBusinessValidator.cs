// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;

namespace Dolittle.Commands.Validation
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