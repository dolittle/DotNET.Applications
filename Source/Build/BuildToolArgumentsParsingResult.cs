using System.Collections.Generic;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class holding the parsing results for the Dolittle.Build Command Line Tool
    /// </summary>
    internal class BuildToolArgumentsParsingResult
    {
        /// <summary>
        /// The path of the Assembly to load from
        /// </summary>
        public string AssemblyPath {get; }
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
        /// <param name="useModules"></param>
        /// <param name="namespaceSegmentsToStrip"></param>
        /// <param name="generateProxies"></param>
        /// <param name="proxiesBasePath"></param>
        public BuildToolArgumentsParsingResult(string assemblyPath, bool useModules, Dictionary<Area, IEnumerable<string>> namespaceSegmentsToStrip, bool generateProxies, string proxiesBasePath)
        {
            AssemblyPath = assemblyPath;
            UseModules = useModules;
            NamespaceSegmentsToStrip = namespaceSegmentsToStrip;
            GenerateProxies = generateProxies;
            ProxiesBasePath = proxiesBasePath;  
        }
    }
}