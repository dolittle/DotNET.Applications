/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents something that should be called during booting
    /// </summary>
    public interface ICanPerformBootProcedure
    {
        /// <summary>
        /// Method that gets called during booting of an application
        /// </summary>
        void Perform();
    }
}