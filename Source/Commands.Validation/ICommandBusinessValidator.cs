// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Marker interface for business validators.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface and also <see cref="ICanValidate{T}"/> will be automatically registered.
    /// You most likely want to subclass <see cref="CommandBusinessValidatorFor{T}"/>.
    /// </remarks>
    public interface ICommandBusinessValidator : ICanValidate, IValidator
    {
    }
}
