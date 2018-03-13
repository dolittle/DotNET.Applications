/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Diagnostics
{
    /// <summary>
    /// Defines the system that works with <see cref="ITypeRuleFor{T}"/>
    /// </summary>
    public interface ITypeRules
    {
        /// <summary>
        /// Validates all <see cref="ITypeRuleFor{T}">rules for types</see> in the system
        /// </summary>
        void ValidateAll();
    }
}
