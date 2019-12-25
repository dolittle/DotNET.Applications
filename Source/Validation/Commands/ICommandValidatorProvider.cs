// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Defines a provider that returns command-specific input and business rule validators.
    /// </summary>
    public interface ICommandValidatorProvider
    {
        /// <summary>
        /// Retrieves an input validator specific to the command.
        /// </summary>
        /// <param name="command">Command to be validates.</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available.</returns>
        ICommandInputValidator GetInputValidatorFor(ICommand command);

        /// <summary>
        /// Retrieves an business-rule validator specific to the command.
        /// </summary>
        /// <param name="command">Command to be validates.</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available.</returns>
        ICommandBusinessValidator GetBusinessValidatorFor(ICommand command);

        /// <summary>
        /// Retrieves an input validator specific to the command type.
        /// </summary>
        /// <param name="type">Type of the Command to be validates.</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available.</returns>
        ICommandInputValidator GetInputValidatorFor(Type type);

        /// <summary>
        /// Retrieves an business-rule validator specific to the command type.
        /// </summary>
        /// <param name="commandType">Type of the Command to be validates.</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available.</returns>
        ICommandBusinessValidator GetBusinessValidatorFor(Type commandType);
    }
}