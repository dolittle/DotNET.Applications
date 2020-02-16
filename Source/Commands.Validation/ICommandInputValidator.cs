// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Defines a marker interface for input level validator for a <see cref="ICommand"/>.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface and also <see cref="ICanValidate{T}"/> will be automatically registered.
    /// You most likely want to subclass <see cref="CommandInputValidatorFor{T}"/>.
    /// </remarks>
    public interface ICommandInputValidator : ICanValidate, IValidator
    {
    }
}