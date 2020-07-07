// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Validation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represents a <see cref="ICommandValidator">ICommandValidationService</see>.
    /// </summary>
    [Singleton]
    public class CommandValidator : ICommandValidator
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;
        readonly ICommandRequestToCommandConverter _commandRequestConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidator"/> class.
        /// </summary>
        /// <param name="commandValidatorProvider"><see cref="ICommandValidatorProvider"/> for providing command validators.</param>
        /// <param name="commandRequestConverter"><see cref="ICommandToCommandRequestConverter"/> for converting to command instances.</param>
        public CommandValidator(
            ICommandValidatorProvider commandValidatorProvider,
            ICommandRequestToCommandConverter commandRequestConverter)
        {
            _commandValidatorProvider = commandValidatorProvider;
            _commandRequestConverter = commandRequestConverter;
        }

        /// <inheritdoc/>
        public CommandValidationResult Validate(CommandRequest command)
        {
            var result = new CommandValidationResult();
            var commandInstance = _commandRequestConverter.Convert(command);

            var validationResults = ValidateInternal(commandInstance);
            result.ValidationResults = validationResults.Where(v => v.MemberNames.First() != ModelRule<object>.ModelRulePropertyName);
            result.CommandErrorMessages = validationResults.Where(v => v.MemberNames.First() == ModelRule<object>.ModelRulePropertyName).Select(v => v.ErrorMessage);
            return result;
        }

        IEnumerable<ValidationResult> ValidateInternal(ICommand command)
        {
            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(command);
            if (inputValidator != null)
            {
                var inputValidationErrors = inputValidator.ValidateFor(command);
                if (inputValidationErrors.Any())
                    return inputValidationErrors;
            }

            var businessValidator = _commandValidatorProvider.GetBusinessValidatorFor(command);
            if (businessValidator != null)
            {
                var businessValidationErrors = businessValidator.ValidateFor(command);
                return businessValidationErrors.Any() ? businessValidationErrors : Array.Empty<ValidationResult>();
            }

            return Array.Empty<ValidationResult>();
        }
    }
}