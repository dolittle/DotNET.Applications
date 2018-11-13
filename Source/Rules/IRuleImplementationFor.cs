/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Rules
{
    /// <summary>
    /// Defines the way to get a rule implementation
    /// </summary>
    public interface IRuleImplementationFor<TDelegate>
    {
        /// <summary>
        /// Get the rule
        /// </summary>
        TDelegate Rule { get; }
    }
}