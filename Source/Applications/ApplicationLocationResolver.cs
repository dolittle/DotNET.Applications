/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationLocationResolver"/>
    /// </summary>
    public class ApplicationLocationResolver : IApplicationLocationResolver
    {
        /// <summary>
        /// The key representing a <see cref="IBoundedContext"/> as part of <see cref="IApplicationStructureMapBuilder"/>
        /// </summary>
        public const string BoundedContextKey = "BoundedContext";

        /// <summary>
        /// The key representing a <see cref="IModule"/> as part of <see cref="IApplicationStructureMapBuilder"/>
        /// </summary>
        public const string ModuleKey = "Module";

        /// <summary>
        /// The key representing a <see cref="IFeature"/> as part of <see cref="IApplicationStructureMapBuilder"/>
        /// </summary>
        public const string FeatureKey = "Feature";

        /// <summary>
        /// The key representing a <see cref="ISubFeature"/> as part of <see cref="IApplicationStructureMapBuilder"/>
        /// </summary>
        public const string SubFeatureKey = "SubFeature";
        
        readonly IApplicationStructureMap _applicationStructureMap;
        

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationLocationResolver"/>
        /// </summary>
        /// <param name="applicationStructureMap"></param>
        public ApplicationLocationResolver(IApplicationStructureMap applicationStructureMap)
        {
            _applicationStructureMap = applicationStructureMap;
        }

        /// <inheritdoc/>
        public bool CanResolve(Type type)
        {
            var @namespace = type.Namespace;
            return _applicationStructureMap.Formats.Any(format => format.Match(@namespace).HasMatches);
        }

        /// <inheritdoc/>
        public IApplicationLocation Resolve(Type type)
        {
            var @namespace = type.Namespace;

            foreach (var format in _applicationStructureMap.Formats)
            {
                var match = format.Match(@namespace);
                if (match.HasMatches)
                {
                    var segments = GetLocationSegmentsFrom(match);
                    var location = new ApplicationLocation(segments);
                    return location;
                }
            }
            
            throw new UnableToResolveApplicationLocationForType(type);
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