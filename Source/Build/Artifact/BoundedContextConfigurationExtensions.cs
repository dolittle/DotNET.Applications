/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Build.Topology;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Extensions for <see cref="BoundedContextTopology"/> that's specific for the Build.Artifact namespace
    /// </summary>
    public static class BoundedContextConfigurationExtensions
    {
        /// <summary>
        /// Returns all <see cref="FeatureDefinition"/> from the <see cref="Applications.Configuration.Topology"/>
        /// </summary>
        public static IEnumerable<FeatureDefinition> RetrieveFeatures(this BoundedContextTopology configuration)
        {
            if (configuration.UseModules) return configuration.Topology.Modules.SelectMany(_ => _.Features);     
            else return  configuration.Topology.Features;
            
        }

        /// <summary>
        /// Returns a <see cref="Dictionary{Feature, FeatureName}"/> where Key is the Feature (id) and Value is the FeatureName of all <see cref="FeatureDefinition"/> in <see cref="Applications.Configuration.Topology"/>
        /// </summary>
        public static Dictionary<Feature, FeatureName> RetrieveAllFeatureIds(this BoundedContextTopology configuration)
        {
            var featureMap = new Dictionary<Feature, FeatureName>();
            
            AddAllFeaturesToMap(RetrieveFeatures(configuration), ref featureMap);

            return featureMap;
        }
        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology 
        /// </summary>
        public static FeatureDefinition FindMatchingFeature(this BoundedContextTopology boundedContextConfiguration, string @namespace)
        {
            var nonMatchingList = new List<string>();
            var featureDef = boundedContextConfiguration.FindMatchingFeature(@namespace, ref nonMatchingList);
            if (featureDef == null) throw new NonMatchingArtifact();
            return featureDef;
        }
        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology 
        /// </summary>
        public static FeatureDefinition FindMatchingFeature(this BoundedContextTopology boundedContextConfiguration, string @namespace, ref List<string> nonMatchingArtifacts)
        {
            var area = new Area(){Value = @namespace.Split(".").First()};
            var segments = @namespace.Split(".").Skip(1).ToArray();
            
            if (boundedContextConfiguration.NamespaceSegmentsToStrip.ContainsKey(area))
            {
                var newSegments = new List<string>(segments);
                foreach (var segment in boundedContextConfiguration.NamespaceSegmentsToStrip[area]) 
                    newSegments.Remove(segment);
                segments = newSegments.ToArray();
            }
            
            if (boundedContextConfiguration.UseModules)
            {
                var matchingModule = boundedContextConfiguration.Topology.Modules
                    .SingleOrDefault(module => module.Name.Value.Equals(segments[0]));
                
                try
                {
                    return FindMatchingFeature(segments.Skip(1).ToArray(), matchingModule.Features);
                } 
                catch (Exception)
                {
                    nonMatchingArtifacts.Add(@namespace);
                    return null;
                }
            }
            try 
            {
                return FindMatchingFeature(segments, boundedContextConfiguration.Topology.Features);
            }
            catch(Exception)
            {
                nonMatchingArtifacts.Add(@namespace);
                return null;
            }
        }

        static FeatureDefinition FindMatchingFeature(string[] segments, IEnumerable<FeatureDefinition> features)
        {
            var featureName = segments.Count() > 0? segments[0] : "";
            if (string.IsNullOrEmpty(featureName)) 
                throw new Exception("Missing feature");
            
            var matchingFeature = features.SingleOrDefault(feature => feature.Name.Value.Equals(segments[0]));

            if (matchingFeature == null) 
                throw new Exception("Missing feature");

            if (segments.Count() == 1) return matchingFeature;

            return FindMatchingFeature(segments.Skip(1).ToArray(), matchingFeature.SubFeatures);
        }
        
        static void AddAllFeaturesToMap(IEnumerable<FeatureDefinition> features, ref Dictionary<Feature, FeatureName> map)
        {
            foreach (var feature in features)
            {
                map.Add(feature.Feature, feature.Name);
                AddAllFeaturesToMap(feature.SubFeatures, ref map);
            }
        }
    }
}