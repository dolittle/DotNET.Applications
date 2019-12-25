// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a property of an artifact.
    /// </summary>
    public class ProxyProperty
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the default value of the property.
        /// </summary>
        public object PropertyDefaultValue { get; set; }
    }
}