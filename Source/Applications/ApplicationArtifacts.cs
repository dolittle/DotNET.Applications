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
        /// <summary>
        /// The key representing a <see cref="IBoundedContext"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string BoundedContextKey = "BoundedContext";

        /// <summary>
        /// The key representing a <see cref="IModule"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string ModuleKey = "Module";

        /// <summary>
        /// The key representing a <see cref="IFeature"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string FeatureKey = "Feature";

        /// <summary>
        /// The key representing a <see cref="ISubFeature"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string SubFeatureKey = "SubFeature";

        readonly IApplication _application;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifacts"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication"/> the resource belongs to</param>
        /// <param name="artifactTypeToTypeMaps"><see cref="IArtifactTypeToTypeMaps"/> for mapping <see cref="IArtifactType"/> to and from <see cref="Type"/></param>
        public ApplicationArtifacts(IApplication application, IArtifactTypeToTypeMaps artifactTypeToTypeMaps)
        {
            _application = application;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
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
            var artifactType = _artifactTypeToTypeMaps.Map(type);
            var location = new ApplicationLocation(new[] { new BoundedContext("Somewhere")});
            var applicationArtifactIdentifier = new ApplicationArtifactIdentifier(this._application, ApplicationAreas.Domain, location, new Artifact(type.Name, artifactType));
            return applicationArtifactIdentifier;

#if(false)
            var @namespace = type.Namespace;

            foreach (var format in _application.Structure.AllStructureFormats)
            {
                var match = format.Match(@namespace);
                if (match.HasMatches)
                {
                    var segments = GetLocationSegmentsFrom(match);
                    var location = new ApplicationLocation(segments);
                    var identifier = new ApplicationArtifactIdentifier(_application, location, new Artifact(type.Name, null));
                    return identifier;
                }
            }

            throw new UnableToIdentifyArtifact(type);
#endif            

            
        }



        IEnumerable<IApplicationLocationSegment> GetLocationSegmentsFrom(ISegmentMatches match)
        {
            var matchAsDictionary = match.AsDictionary();

            var segments = new List<IApplicationLocationSegment>();
            BoundedContext boundedContext = null;
            Module module = null;
            Feature feature = null;
            List<SubFeature> subFeatures = new List<SubFeature>();

            if (matchAsDictionary.ContainsKey(BoundedContextKey))
            {
                boundedContext = new BoundedContext(matchAsDictionary[BoundedContextKey].Single());
                segments.Add(boundedContext);

                if (matchAsDictionary.ContainsKey(ModuleKey))
                {
                    module = new Module(boundedContext, matchAsDictionary[ModuleKey].Single());
                    segments.Add(module);

                    if (matchAsDictionary.ContainsKey(FeatureKey))
                    {
                        feature = new Feature(module, matchAsDictionary[FeatureKey].Single());
                        segments.Add(feature);

                        if (matchAsDictionary.ContainsKey(SubFeatureKey))
                        {
                            foreach (var subFeatureName in matchAsDictionary[SubFeatureKey])
                            {
                                var subFeature = new SubFeature(feature, subFeatureName);
                                segments.Add(subFeature);
                            }
                        }
                    }
                }
            }

            return segments;
        }
    }
}
