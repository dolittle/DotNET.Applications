/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a property of an artifact
    /// </summary>
    public class ProxyProperty
    {
        /// <summary>
        /// Gets and sets the name of the property
        /// </summary>
        public string PropertyName {get; set;}
        /// <summary>
        /// Gets and sets the default value of the property 
        /// </summary>
        public object PropertyDefaultValue {get; set;}
    }
}