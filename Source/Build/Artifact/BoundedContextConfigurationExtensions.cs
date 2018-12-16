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
        public static IDictionary<Feature, FeatureDefinition> RetrieveFeatures(this BoundedContextTopology configuration)
        {
            if (configuration.UseModules) return configuration.Topology.Modules.SelectMany(_ => _.Value.Features).ToDictionary(_ => _.Key, _ => _.Value);
            else return  configuration.Topology.Features;
        }

        /// <summary>
        /// Returns a <see cref="Dictionary{Feature, FeatureName}"/> where Key is the Feature (id) and Value is the FeatureName of all <see cref="FeatureDefinition"/> in <see cref="Applications.Configuration.Topology"/>
        /// </summary>
        public static Dictionary<Feature, FeatureName> RetrieveAllFeatureIds(this BoundedContextTopology configuration)
        {
            var featureMap = new Dictionary<Feature, FeatureName>();
            
            AddAllFeaturesToMap(RetrieveFeatures(configuration), featureMap);

            return featureMap;
        }
        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology 
        /// </summary>
        public static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(this BoundedContextTopology boundedContextConfiguration, string @namespace)
        {
            var nonMatchingList = new List<string>();
            var featureDef = boundedContextConfiguration.FindMatchingFeature(@namespace, nonMatchingList);
            if (featureDef.Key == null) throw new NonMatchingArtifact();
            return featureDef;
        }
        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology 
        /// </summary>
        public static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(this BoundedContextTopology boundedContextConfiguration, string @namespace, List<string> nonMatchingArtifacts)
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
                    .SingleOrDefault(module => module.Value.Name.Value.Equals(segments[0]));
                
                try
                {
                    return FindMatchingFeature(segments.Skip(1).ToArray(), matchingModule.Value.Features);
                } 
                catch (Exception)
                {
                    nonMatchingArtifacts.Add(@namespace);
                    return new KeyValuePair<Feature, FeatureDefinition>(null, null);
                }
            }
            try 
            {
                return FindMatchingFeature(segments, boundedContextConfiguration.Topology.Features);
            }
            catch(Exception)
            {
                nonMatchingArtifacts.Add(@namespace);
                return new KeyValuePair<Feature, FeatureDefinition>(null, null);
            }
        }

        static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(string[] segments, IDictionary<Feature, FeatureDefinition> features)
        {
            var featureName = segments.Count() > 0? segments[0] : "";
            if (string.IsNullOrEmpty(featureName)) 
                throw new Exception("Missing feature");
            
            Func<KeyValuePair<Feature, FeatureDefinition>, bool>  predicate = (KeyValuePair<Feature, FeatureDefinition> feature) => feature.Value.Name.Value.Equals(segments[0]);
            if( !features.Any(predicate) )  throw new Exception("Missing feature");

            var matchingFeature = features.SingleOrDefault(predicate);

            if (segments.Count() == 1) return matchingFeature;

            return FindMatchingFeature(segments.Skip(1).ToArray(), matchingFeature.Value.SubFeatures);
        }
        
        static void AddAllFeaturesToMap(IDictionary<Feature,FeatureDefinition> features, Dictionary<Feature, FeatureName> map)
        {
            foreach (var feature in features)
            {
                map.Add(feature.Key, feature.Value.Name);
                AddAllFeaturesToMap(feature.Value.SubFeatures, map);
            }
        }
    }
}