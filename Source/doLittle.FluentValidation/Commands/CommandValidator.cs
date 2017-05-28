/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using doLittle.Commands;
using doLittle.Execution;
using doLittle.Validation;

namespace doLittle.FluentValidation.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandValidator">ICommandValidationService</see>
    /// </summary>
    [Singleton]
    public class CommandValidator : ICommandValidator
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;
        readonly ICommandRequestConverter _commandRequestConverter;

        /// <summary>
        /// Initializes an instance of <see cref="CommandValidator"/> CommandValidationService
        /// </summary>
        /// <param name="commandValidatorProvider"><see cref="ICommandValidatorProvider"/> for providing command validators</param>
        /// <param name="commandRequestConverter"><see cref="ICommandRequestConverter"/> for converting to command instances</param>
        public CommandValidator(
            ICommandValidatorProvider commandValidatorProvider,
            ICommandRequestConverter commandRequestConverter)
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
                if (inputValidationErrors.Count() > 0)
                    return inputValidationErrors;
            }

            var businessValidator = _commandValidatorProvider.GetBusinessValidatorFor(command);
            if (businessValidator != null)
            {
                var businessValidationErrors = businessValidator.ValidateFor(command);
                return businessValidationErrors.Count() > 0 ? businessValidationErrors : new ValidationResult[0];
            }

            return new ValidationResult[0];
        }
    }
}