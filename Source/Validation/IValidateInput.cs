/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Validation
{
    /// <summary>
    /// Marker interface to identify types that can perform input validation.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered and used for validation of properties
    /// of types (i.e. commands) for which there are no explicitly defined validators.
    /// You most likely want to subclass <see cref="InputValidator{T}"/>.
    /// </remarks>
    public interface IValidateInput<T>
    {
    }
}
