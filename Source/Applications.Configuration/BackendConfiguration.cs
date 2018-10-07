/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the configuration for the <see cref="BoundedContext"/> backend
    /// </summary>
    public class BackendConfiguration
    {
        /// <summary>
        /// The backend programming language used in the backend
        /// </summary>
        public string Language {get; set;}
        /// <summary>
        /// The entrypoint of the <see cref="BoundedContext"/>
        /// </summary>
        public string EntryPoint {get; set;}
    }
}