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

        readonly IArtifactsConfigurationManager _artifactsConfigurationManager;
        readonly IArtifactTypeMap _artifactTypeMap;

        readonly IEnumerable<PropertyInfo>  _artifactProperties = typeof(ArtifactsByTypeDefinition).GetProperties().Where(_ => _.PropertyType == typeof(IEnumerable<ArtifactDefinition>));

        /// <summary>
        /// Initializes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="artifactsConfigurationManager"><see cref="IArtifactsConfigurationManager"/></param>
        /// <param name="artifactTypeMap"></param>
        public BootProcedure(IArtifactsConfigurationManager artifactsConfigurationManager, IArtifactTypeMap artifactTypeMap)
        {
            _artifactsConfigurationManager = artifactsConfigurationManager;
            _artifactTypeMap = artifactTypeMap;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            var config = _artifactsConfigurationManager.Load();
            var artifacts = new List<ArtifactDefinition>();
            config.Artifacts.Select(_ => _.Value).ForEach(artifactByType => 
            {
                _artifactProperties.ForEach(property => artifacts.AddRange(property.GetValue(artifactByType) as IEnumerable<ArtifactDefinition>));
            });

            artifacts.ForEach(_ => _artifactTypeMap.Register(new Artifact(_.Artifact, _.Generation), _.Type.GetActualType()));

            HasPerformed = true;
        }
    }
}