// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation.Validators;

namespace Dolittle.FluentValidation.for_PropertyValidatorWithDynamicState
{
    public class MyValidator : PropertyValidatorWithDynamicState
    {
        public bool Something { get; set; }

        public MyValidator()
            : base("")
        {
        }

        public string TheString { get; private set; }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            TheString = DynamicState.TheString;
            return true;
        }
    }
}
