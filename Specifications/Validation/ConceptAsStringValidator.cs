/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : BusinessValidator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            RuleFor(c => c.Value).NotEmpty();
        }
    }
}
