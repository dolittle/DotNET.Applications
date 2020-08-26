// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Microservice.Configuration;

namespace Dolittle.Build
{
    /// <summary>
    /// Defines the loader for the <see cref="MicroserviceConfiguration"/>.
    /// </summary>
    public interface IMicroserviceLoader
    {
        /// <summary>
        /// Loads the <see cref="MicroserviceConfiguration"/> from disk.
        /// </summary>
        /// <returns>The loaded <see cref="MicroserviceConfiguration"/>.</returns>
        MicroserviceConfiguration Load();

        /// <summary>
        /// Loads the <see cref="MicroserviceConfiguration"/> from disk.
        /// </summary>
        /// <param name="relativePath">The relative path to the microservice.json file.</param>
        /// <returns>The loaded <see cref="MicroserviceConfiguration"/>.</returns>
        MicroserviceConfiguration Load(string relativePath);
    }
}