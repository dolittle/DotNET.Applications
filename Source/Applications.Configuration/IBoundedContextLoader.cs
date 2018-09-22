/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Defines the loader for the <see cref="BoundedContextConfiguration"/>
    /// </summary>
    public interface IBoundedContextLoader
    {
        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from disk
        /// /// <param name="solutionDirPath"></param>
        /// </summary>
        BoundedContextConfiguration Load(string solutionDirPath);
    }
}