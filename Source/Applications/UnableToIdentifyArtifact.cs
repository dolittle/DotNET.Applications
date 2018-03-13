/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Gets thrown when its impossible to identify an <see cref="IArtifact"/>
    /// </summary>
    public class UnableToIdentifyArtifact : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnableToIdentifyArtifact"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that is not possible to identify</param>
        public UnableToIdentifyArtifact(Type type) : base($"Unable to identify application artifact for type '{type.FullName}'") { }

        /// <summary>
        /// Initializes a new instance of <see cref="UnableToIdentifyArtifact"/>
        /// </summary>
        /// <param name="identifierString"><see cref="string"/> that is not possible to identify</param>
        public UnableToIdentifyArtifact(string identifierString) : base($"Unable to identify application artifact for string '{identifierString}'. Expected format should be: {ApplicationArtifactIdentifierStringConverter.ExpectedFormat}") { }
    }
}
