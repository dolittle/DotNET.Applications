// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Defines an interface to add composed child validators to a parent validator.
    /// </summary>
    public interface IComposedCommandRuleBuilder
    {
        /// <summary>
        /// Builds composed validators from <paramref name="childValidators"/> and adds them to <paramref name="validator"/>.
        /// </summary>
        /// <param name="validator"><see cref="AbstractValidator{T}"/> to add to.</param>
        /// <param name="childValidators"><see cref="IEnumerable{T}"/> of <see cref="IValidator"/> with child validators to add.</param>
        /// <typeparam name="TCommand">The type of command to build validators of.</typeparam>
        void AddTo<TCommand>(AbstractValidator<TCommand> validator, IEnumerable<IValidator> childValidators);
    }
}
