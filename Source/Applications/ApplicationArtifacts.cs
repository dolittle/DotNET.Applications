/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Artifacts;
using doLittle.Strings;

namespace doLittle.Applications
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


        IApplication _application;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifacts"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication"/> the resource belongs to</param>
        /// <param name="applicationArtifactTypes"><see cref="IArtifactTypes"/> for identifying <see cref="IArtifactType"/></param>
        public ApplicationArtifacts(IApplication application, IArtifactTypes applicationArtifactTypes)
        {
            _application = application;
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
#endif            

            throw new UnableToIdentifyArtifact(type);
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
