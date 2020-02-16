// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represents an implementation of <see cref="IValidatorFactory"/> for dealing with validation for commands.
    /// </summary>
    public class CommandValidatorFactory : IValidatorFactory
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidatorFactory"/> class.
        /// </summary>
        /// <param name="commandValidatorProvider"><see cref="ICommandValidatorProvider"/> to get validators from.</param>
        public CommandValidatorFactory(ICommandValidatorProvider commandValidatorProvider)
        {
            _commandValidatorProvider = commandValidatorProvider;
        }

        /// <inheritdoc/>
        public IValidator<T> GetValidator<T>()
        {
            return _commandValidatorProvider.GetInputValidatorFor(typeof(T)) as IValidator<T>;
        }

        /// <inheritdoc/>
        public IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                return _commandValidatorProvider.GetInputValidatorFor(type) as IValidator;
            }

            return null;
        }
    }
}