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
using Dolittle.Execution;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationLocationResolver"/>
    /// </summary>
    [Singleton]
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
        readonly IApplication _application;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationLocationResolver"/>
        /// </summary>
        /// <param name="application"></param>
        /// <param name="applicationStructureMap"></param>
        public ApplicationLocationResolver(IApplication application, IApplicationStructureMap applicationStructureMap)
        {
            _applicationStructureMap = applicationStructureMap;
            _application = application;
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
            var segments = new List<IApplicationLocationSegment>();
            List<SubFeature> subFeatures = new List<SubFeature>();

            segments.AddRange(_application.Prefixes.Select(_ => new BoundedContext(_.Name.AsString())));
            IApplicationLocationSegment previousSegment = segments.SingleOrDefault(_ => _ is BoundedContext);

            match.ForEach(stringSegment => 
            {
                IApplicationLocationSegment currentSegment = null;

                switch( stringSegment.Identifier )
                {
                    case BoundedContextKey : currentSegment = new BoundedContext(stringSegment.Values.Single()); break;
                    case ModuleKey : currentSegment = new Module((BoundedContext)previousSegment, stringSegment.Values.Single()); break;
                    case FeatureKey : currentSegment = new Feature(previousSegment, stringSegment.Values.Single()); break;
                    case SubFeatureKey : 
                        foreach (var segmentString in stringSegment.Values)
                        {
                            currentSegment = new SubFeature((IFeature)previousSegment, segmentString);

                            if (segmentString == stringSegment.Values.Last()) break; // If it's the last one, let the statement outside this switch handle it
                            segments.Add(currentSegment);
                            previousSegment = currentSegment;
                        }
                        break;
                }
                if( currentSegment != null )
                {
                    segments.Add(currentSegment);
                    previousSegment = currentSegment;
                }
            });

            return segments;
        }
    }
}