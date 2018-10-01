/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation
{
    public class ObjectValidator : BusinessValidator<object>
    {
        public ObjectValidator()
        {
            ModelRule()
                .NotNull();
        }
    }
}
