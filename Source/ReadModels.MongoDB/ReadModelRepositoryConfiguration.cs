// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents a resource configuration for a MongoDB Read model implementation.
    /// </summary>
    public class ReadModelRepositoryConfiguration
    {
        /// <summary>
        /// Gets or sets the Database name.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the connection string for the configuration.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the Host String.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to use SSL.
        /// </summary>
        public bool UseSSL { get; set; }
    }
}
