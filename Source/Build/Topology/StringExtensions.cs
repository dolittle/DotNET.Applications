/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Extensions for <see cref="string"/> that's specific for the Dolittle.Build.Topology namespace
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extracts a <see cref="ModuleDefinition"/> from the string path
        /// </summary>
        public static KeyValuePair<Module, ModuleDefinition> GetModuleFromPath(this string path)
        {
            if ( string.IsNullOrEmpty(path)
                || path.Contains(' ')
                || path.Contains('-')) 
                throw new ArgumentException($"Could not get module from path: '{path}'. Path cannot be empty, contain spaces or dashes");
            var splitPath = path.Split('.');
            var moduleName = splitPath.First();

            var features = new Dictionary<Feature, FeatureDefinition>();
            var featurePath = string.Join(".", splitPath.Skip(1));
            if (!string.IsNullOrEmpty(featurePath))
            {
                var feature = featurePath.GetFeatureFromPath();
                features[feature.Key] = feature.Value;
            }
            Module moduleId = Guid.NewGuid();
            var module = new ModuleDefinition(moduleName, features);

            return new KeyValuePair<Module, ModuleDefinition>(moduleId, module);
        }
        /// <summary>
        /// Extracts a <see cref="FeatureDefinition"/> from the string path
        /// </summary>
        public static KeyValuePair<Feature, FeatureDefinition> GetFeatureFromPath(this string path)
        {
            if ( string.IsNullOrEmpty(path)
                || path.Contains(' ')
                || path.Contains('-')) 
                throw new ArgumentException($"Could not get module from path: '{path}'. Path cannot be empty, contain spaces or dashes");
            var stringSegmentsReversed = path.Split('.').Reverse().ToArray();

            FeatureDefinition currentFeature = null;
            Feature currentFeatureId = null;
            
            foreach (var featureName in stringSegmentsReversed )
            {
                var subFeatures = new Dictionary<Feature, FeatureDefinition>();
                if( currentFeature != null )
                {
                    subFeatures[currentFeatureId] = currentFeature;
                }
                currentFeatureId = Guid.NewGuid();
                currentFeature = new FeatureDefinition(featureName, subFeatures);
            }

            return new KeyValuePair<Feature, FeatureDefinition>(currentFeatureId, currentFeature);
        }
    }
}