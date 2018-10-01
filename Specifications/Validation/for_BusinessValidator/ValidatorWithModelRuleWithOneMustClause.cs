/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.for_BusinessValidator
{
    public class ValidatorWithModelRuleWithOneMustClause : BusinessValidator<SimpleObject>
    {
        public bool CallbackCalled = false;
        public ValidatorWithModelRuleWithOneMustClause()
        {
            ModelRule().Must(o => CallbackCalled = true);
        }
    }
}
