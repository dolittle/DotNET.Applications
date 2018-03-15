/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifacts"/>
    /// </summary>
    public class ApplicationArtifacts : IApplicationArtifacts
    {
        readonly IApplication _application;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;
        readonly IApplicationLocationResolver _applicationLocationResolver;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifacts"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication"/> the resource belongs to</param>
        /// <param name="artifactTypeToTypeMaps"><see cref="IArtifactTypeToTypeMaps"/> for mapping <see cref="IArtifactType"/> to and from <see cref="Type"/></param>
        /// <param name="applicationLocationResolver"></param>
        public ApplicationArtifacts(
            IApplication application,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            IApplicationLocationResolver applicationLocationResolver)
        {
            _application = application;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
            _applicationLocationResolver = applicationLocationResolver;
        }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier Identify(object resource)
        {
            var type = resource.GetType();
            return Identify(type);
        }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier Identify(Type type)
        {
            if( _applicationLocationResolver.CanResolve(type) ) 
            {
                var artifactType = _artifactTypeToTypeMaps.Map(type);
                var location = _applicationLocationResolver.Resolve(type);
                var applicationArtifactIdentifier = new ApplicationArtifactIdentifier(_application, ApplicationAreas.Domain, location, new Artifact(type.Name, artifactType));
                return applicationArtifactIdentifier;
            }

            throw new UnableToIdentifyArtifact(type);
        }
    }
}
