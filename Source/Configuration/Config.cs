/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Applications;
using Dolittle.Strings;

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
        /// Gets the name of the <see cref="Applications.BoundedContext"/> of the running host
        /// </summary>
        public BoundedContextName BoundedContext { get; set; } = BoundedContextName.NotSet;

        /// <summary>
        /// The name of the Domain area
        /// </summary>
        /// <value></value>        
        public string DomainAreaName {get; set; } = "Domain";

        /// <summary>
        /// The name of the Events area
        /// </summary>
        /// <value></value>
        public string EventsAreaName {get; set; } = "Events";

        /// <summary>
        /// The name of the Read area
        /// </summary>
        /// <value></value>
        public string ReadAreaName {get; set; } = "Read";

        /// <summary>
        /// The name of the Frontend area
        /// </summary>
        /// <value></value>
        public string FrontendAreaName {get; set; } = "Web";
    }
}