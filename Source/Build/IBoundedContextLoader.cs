// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Applications.Configuration;

namespace Dolittle.Build
{
    /// <summary>
    /// Defines the loader for the <see cref="BoundedContextConfiguration"/>.
    /// </summary>
    public interface IBoundedContextLoader
    {
        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from disk.
        /// </summary>
        /// <returns>The loaded <see cref="BoundedContextConfiguration"/>.</returns>
        BoundedContextConfiguration Load();

        /// <summary>
        /// Loads the <see cref="BoundedContextConfiguration"/> from disk.
        /// </summary>
        /// <param name="relativePath">The relative path to the bounded-context.json file.</param>
        /// <returns>The loaded <see cref="BoundedContextConfiguration"/>.</returns>
        BoundedContextConfiguration Load(string relativePath);
    }
}