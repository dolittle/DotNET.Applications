/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents the general configuration
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets the <see cref="ApplicationName"/> of the <see cref="IApplication"/>
        /// </summary>
        public ApplicationName Application { get; set; } = ApplicationName.NotSet;

        /// <summary>
        /// Gets the name of the <see cref="BoundedContext"/> of the running host
        /// </summary>
        public BoundedContextName BoundedContext { get; set; } = BoundedContextName.NotSet;
    }
}