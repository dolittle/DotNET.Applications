// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Build.Topology;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Extensions for <see cref="BoundedContextTopology"/> that's specific for the Build.Artifact namespace.
    /// </summary>
    public static class BoundedContextConfigurationExtensions
    {
        /// <summary>
        /// Returns all <see cref="FeatureDefinition"/> from the <see cref="Applications.Configuration.Topology"/>.
        /// </summary>
        /// <param name="configuration"><see cref="BoundedContextTopology"/> to extend.</param>
        /// <returns>A map of <see cref="Feature"/> to <see cref="FeatureDefinition"/>.</returns>
        public static IDictionary<Feature, FeatureDefinition> RetrieveFeatures(this BoundedContextTopology configuration)
        {
            if (configuration.UseModules) return configuration.Topology.Modules.SelectMany(_ => _.Value.Features).ToDictionary(_ => _.Key, _ => _.Value);
            else return configuration.Topology.Features;
        }

        /// <summary>
        /// Returns a <see cref="Dictionary{Feature, FeatureName}"/> where Key is the Feature (id) and Value is the FeatureName of all <see cref="FeatureDefinition"/> in <see cref="Applications.Configuration.Topology"/>.
        /// </summary>
        /// <param name="configuration"><see cref="BoundedContextTopology"/> to extend.</param>
        /// <returns>A map of <see cref="Feature"/> to <see cref="FeatureName"/>.</returns>
        public static Dictionary<Feature, FeatureName> RetrieveAllFeatureIds(this BoundedContextTopology configuration)
        {
            var featureMap = new Dictionary<Feature, FeatureName>();

            AddAllFeaturesToMap(RetrieveFeatures(configuration), featureMap);

            return featureMap;
        }

        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology.
        /// </summary>
        /// <param name="configuration"><see cref="BoundedContextTopology"/> to extend.</param>
        /// <param name="namespace">Namespace to find matching feature in.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> of <see cref="Feature"/> to <see cref="FeatureDefinition"/>.</returns>
        public static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(this BoundedContextTopology configuration, string @namespace)
        {
            var nonMatchingList = new List<string>();
            var featureDef = configuration.FindMatchingFeature(@namespace, nonMatchingList);
            if (featureDef.Key == null) throw new NonMatchingArtifact();
            return featureDef;
        }

        /// <summary>
        /// Returns a <see cref="FeatureDefinition"/> that matches the artifact with the given namespace based on the <see cref="BoundedContextTopology">BoundedContextConfiguration's </see> topology.
        /// </summary>
        /// <param name="configuration"><see cref="BoundedContextTopology"/> to extend.</param>
        /// <param name="namespace">Namespace to find matching feature in.</param>
        /// <param name="artifactsWithoutMatch">The artifacts that was unmatched.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> of <see cref="Feature"/> to <see cref="FeatureDefinition"/>.</returns>
        public static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(
            this BoundedContextTopology configuration,
            string @namespace,
            List<string> artifactsWithoutMatch)
        {
            var area = new Area() { Value = @namespace.Split('.').First() };
            var segments = @namespace.Split('.').Skip(1).ToArray();

            if (configuration.NamespaceSegmentsToStrip.ContainsKey(area))
            {
                var newSegments = new List<string>(segments);
                foreach (var segment in configuration.NamespaceSegmentsToStrip[area])
                    newSegments.Remove(segment);
                segments = newSegments.ToArray();
            }

            if (configuration.UseModules)
            {
                var matchingModule = configuration.Topology.Modules
                    .SingleOrDefault(module => module.Value.Name.Value.Equals(segments[0], StringComparison.InvariantCulture));

                try
                {
                    return FindMatchingFeature(segments.Skip(1).ToArray(), matchingModule.Value.Features);
                }
                catch (Exception)
                {
                    artifactsWithoutMatch.Add(@namespace);
                    return new KeyValuePair<Feature, FeatureDefinition>(null, null);
                }
            }

            try
            {
                return FindMatchingFeature(segments, configuration.Topology.Features);
            }
            catch (Exception)
            {
                artifactsWithoutMatch.Add(@namespace);
                return new KeyValuePair<Feature, FeatureDefinition>(null, null);
            }
        }

        static KeyValuePair<Feature, FeatureDefinition> FindMatchingFeature(string[] segments, IDictionary<Feature, FeatureDefinition> features)
        {
            var featureName = segments.Length > 0 ? segments[0] : string.Empty;
            if (string.IsNullOrEmpty(featureName))
                throw new MissingFeature(featureName);

            bool predicate(KeyValuePair<Feature, FeatureDefinition> feature) => feature.Value.Name.Value.Equals(segments[0], StringComparison.InvariantCulture);
            if (!features.Any(predicate)) throw new MissingFeature(featureName);

            var matchingFeature = features.SingleOrDefault(predicate);

            if (segments.Length == 1) return matchingFeature;

            return FindMatchingFeature(segments.Skip(1).ToArray(), matchingFeature.Value.SubFeatures);
        }

        static void AddAllFeaturesToMap(IDictionary<Feature, FeatureDefinition> features, Dictionary<Feature, FeatureName> map)
        {
            foreach (var feature in features)
            {
                map.Add(feature.Key, feature.Value.Name);
                AddAllFeaturesToMap(feature.Value.SubFeatures, map);
            }
        }
    }
}