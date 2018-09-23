/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class holding the parsing results for the Dolittle.Build Command Line Tool
    /// </summary>
    public class BuildToolArgumentsParsingResult
    {
        /// <summary>
        /// The path of the Assembly to load from
        /// </summary>
        public string AssemblyPath {get; }
        /// <summary>
        /// The relative path to the bounded-context.json configuration file
        /// </summary>
        /// <value></value>
        public string BoundedContextConfigRelativePath {get; }
        /// <summary>
        /// Whether or not the Topology should build using Modules or not
        /// </summary>
        public bool UseModules {get; }
        /// <summary>
        /// A mapping from <see cref="Area"/> to a string representing a segment in the namespace that the user wishes to exclude from the Module/Feature
        /// </summary>
        public IDictionary<Area, IEnumerable<string>> NamespaceSegmentsToStrip {get; }
        /// <summary>
        /// Whether or not one wants to generate proxies
        /// </summary>
        public bool GenerateProxies {get; }
        /// <summary>
        /// The base path for proxies
        /// </summary>
        public string ProxiesBasePath {get; }
        
        /// <summary>
        /// Instantiates an instance of <see cref="BuildToolArgumentsParsingResult"/>
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="boundedContextConfigRelativePath"/>
        /// <param name="useModules"></param>
        /// <param name="namespaceSegmentsToStrip"></param>
        /// <param name="generateProxies"></param>
        /// <param name="proxiesBasePath"></param>
        public BuildToolArgumentsParsingResult(string assemblyPath, string boundedContextConfigRelativePath, bool useModules, Dictionary<Area, IEnumerable<string>> namespaceSegmentsToStrip, bool generateProxies, string proxiesBasePath)
        {
            AssemblyPath = assemblyPath;
            BoundedContextConfigRelativePath = boundedContextConfigRelativePath;
            UseModules = useModules;
            NamespaceSegmentsToStrip = namespaceSegmentsToStrip;
            GenerateProxies = generateProxies;
            ProxiesBasePath = proxiesBasePath;  
        }
    }
}