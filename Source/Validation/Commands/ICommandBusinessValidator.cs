/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Validation;
using FluentValidation;

namespace doLittle.FluentValidation.Commands
{
    /// <summary>
    /// Marker interface for business validators.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface and also <see cref="ICanValidate{T}"/> will be automatically registered.
    /// You most likely want to subclass <see cref="CommandBusinessValidator{T}"/>.
    /// </remarks>
    public interface ICommandBusinessValidator : ICanValidate, IValidator
    {
    }
}
