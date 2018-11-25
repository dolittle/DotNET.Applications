/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Artifacts.Configuration;
using Dolittle.Bootstrapping;
using Dolittle.Collections;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents the <see cref="ICanPerformBootProcedure">boot procedure</see> for artifacts
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        /// <summary>
        /// Gets whether or not this <see cref="ICanPerformBootProcedure">boot procedure</see> has performed
        /// </summary>
        public static bool HasPerformed { get; private set; }

        readonly IArtifactTypeMap _artifactTypeMap;

        readonly IEnumerable<PropertyInfo>  _artifactProperties = typeof(ArtifactsByTypeDefinition).GetProperties().Where(_ => _.PropertyType == typeof(IEnumerable<ArtifactDefinition>));
        readonly ArtifactsConfiguration _artifactsConfiguration;

        /// <summary>
        /// Initializes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="artifactsConfiguration">The <see cref="ArtifactsConfiguration"/> </param>
        /// <param name="artifactTypeMap"></param>
        public BootProcedure(ArtifactsConfiguration artifactsConfiguration, IArtifactTypeMap artifactTypeMap)
        {
            _artifactsConfiguration = artifactsConfiguration;
            _artifactTypeMap = artifactTypeMap;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            var artifacts = new List<ArtifactDefinition>();
            _artifactsConfiguration.Artifacts.Select(_ => _.Value).ForEach(artifactByType => 
            {
                _artifactProperties.ForEach(property => artifacts.AddRange(property.GetValue(artifactByType) as IEnumerable<ArtifactDefinition>));
            });

            artifacts.ForEach(_ => _artifactTypeMap.Register(new Artifact(_.Artifact, _.Generation), _.Type.GetActualType()));

            HasPerformed = true;
        }
    }
}