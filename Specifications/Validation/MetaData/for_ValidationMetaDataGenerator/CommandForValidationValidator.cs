// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandForValidationValidator : CommandInputValidatorFor<CommandForValidation>
    {
        public const string NotEmptyErrorMessage = "Should not be empty";
        public const string EmailAddressErrorMessage = "Not a valid email";

        public const int LessThanValue = 50;
        public const int GreaterThanValue = 5;

        public const string LessThanErrorMessage = "Should be less than";
        public const string GreaterThanErrorMessage = "Should be greater than";

        public CommandForValidationValidator()
        {
            RuleFor(o => o.SomeString)
                .NotEmpty()
                    .WithMessage(NotEmptyErrorMessage)
                .EmailAddress()
                    .WithMessage(EmailAddressErrorMessage);

            RuleFor(o => o.SomeInt)
                .LessThan(LessThanValue)
                    .WithMessage(LessThanErrorMessage)
                .GreaterThan(GreaterThanValue)
                    .WithMessage(GreaterThanErrorMessage);
        }
    }
}
