/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Applications;

namespace Dolittle.Hosting
{

    /// <summary>
    /// The exception that gets thrown if the <see cref="ApplicationName"/> has not been set
    /// </summary>
    public class ApplicationNameNotSet : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationNameNotSet"/>
        /// </summary>
        public ApplicationNameNotSet()
        {
        }
    }
}