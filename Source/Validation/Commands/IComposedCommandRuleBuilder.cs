/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using FluentValidation;

namespace doLittle.Commands.Validation
{
    /// <summary>
    /// Defines an interface to add composed child validators to a parent validator.
    /// </summary>
    public interface IComposedCommandRuleBuilder
    {
        /// <summary>
        /// Builds composed validators from <paramref name="childValidators"/> and adds them to <paramref name="validator"/>.
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="childValidators"></param>
        /// <typeparam name="TCommand">The type of command to build validators of.</typeparam>
        void AddTo<TCommand>(AbstractValidator<TCommand> validator, IEnumerable<IValidator> childValidators);
    }
}
