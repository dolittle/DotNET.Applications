/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static ModuleDefinition GetModuleFromPath(this string path)
        {
            if ( string.IsNullOrEmpty(path)
                || path.Contains(' ')
                || path.Contains('-')) 
                throw new ArgumentException($"Could not get module from path: '{path}'. Path cannot be empty, contain spaces or dashes");
            var splitPath = path.Split(".");
            var moduleName = splitPath.First();
            var module = new ModuleDefinition()
            {
                Module = Guid.NewGuid(),
                Name = moduleName
            };
            
            var featurePath = string.Join(".", splitPath.Skip(1));
            if (!string.IsNullOrEmpty(featurePath))
            {
                module.Features = new List<FeatureDefinition>()
                {
                    featurePath.GetFeatureFromPath()
                };
            }

            return module;
        }
        /// <summary>
        /// Extracts a <see cref="FeatureDefinition"/> from the string path
        /// </summary>
        public static FeatureDefinition GetFeatureFromPath(this string path)
        {
            if ( string.IsNullOrEmpty(path)
                || path.Contains(' ')
                || path.Contains('-')) 
                throw new ArgumentException($"Could not get module from path: '{path}'. Path cannot be empty, contain spaces or dashes");
            var stringSegmentsReversed = path.Split(".").Reverse().ToArray();
            
            var currentFeature = new FeatureDefinition()
            {
                Feature = Guid.NewGuid(), 
                Name = stringSegmentsReversed[0]
            };
            foreach (var featureName in stringSegmentsReversed.Skip(1))
            {
                var parentFeature = new FeatureDefinition()
                {
                    Feature = Guid.NewGuid(),
                    Name = featureName,
                };
                parentFeature.SubFeatures = new List<FeatureDefinition>(){currentFeature};
                currentFeature = parentFeature;
            }

            return currentFeature;
        }
    }
}