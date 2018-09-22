using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build
{
    internal static class BuildToolArgumentsParser
    {
        static readonly Regex argumentRegex = new Regex(@"--(\w*)=(.*)", RegexOptions.Compiled); 
        public static BuildToolArgumentsParsingResult Parse(string[] args)
        {
            var assemblyPath = args[0];
            args = args.Skip(1).ToArray();
            var useModules = HandleUseModules(args);
            var namespaceSegmentsToStrip = HandleNamespaceSegmentsToStrip(args);
            var generateProxies = HandleGenerateProxies(args);
            var proxiesBasePath = HandleProxiesBasePath(args);

            return new BuildToolArgumentsParsingResult(assemblyPath, useModules, namespaceSegmentsToStrip, generateProxies, proxiesBasePath);
        }

        static bool HandleUseModules(string[] args)
        {
            var value = GetArgValue(args, "useModules");

            return bool.Parse(value);
            
        }

        static Dictionary<Area, IEnumerable<string>> HandleNamespaceSegmentsToStrip(string[] args)
        {
            var value = GetArgValue(args, "namespaceSegmentsToStrip");

            return ParseValueAsNamespaceSegmentsToStrip(value);
        }

        static bool HandleGenerateProxies(string[] args)
        {
            var value = GetArgValue(args, "generateProxies");

            return bool.Parse(value);
        }

        static string HandleProxiesBasePath(string[] args)
        {
            return GetArgValue(args, "proxiesBasePath");
        }

        static string GetArgValue(string[] args, string argumentName)
        {
            foreach (var match in args.Select(arg => argumentRegex.Match(arg)))
            {
                if (! match.Success) throw new ArgumentException("A argument could not be parsed, it must follow this format: --arg=value");
                var currentArgumentName = match.Groups[1].Value;
                
                if (currentArgumentName == argumentName)
                    return match.Groups.Count == 3? match.Groups[2].Value : "";
                
            }
            throw new ArgumentException($"The argument '{argumentName}' was was not present in the arguments to Dolittle Build Tool");
        }
        
        static Dictionary<Area, IEnumerable<string>> ParseValueAsNamespaceSegmentsToStrip(string value)
        {
            const char separator = '|';

            var namespaceSegmentsToStrip = new Dictionary<Area, IEnumerable<string>>();

            var segments = value.Split(separator);

            foreach (var segment in segments)
            {
                var splittedSegment = segment.Split('=');
                if (splittedSegment.Length != 2) throw new ArgumentException("Errors while parsing NamespaceSegmentsToStrip; It should look like this:\n<NamespaceSegmentsToStrip>NamespacePrefix1=This|NamespacePrefix2=Other");
                var area = new Area()
                {
                    Value = splittedSegment[0]
                };
                var namespaceSegment = splittedSegment[1];
                
                if (! namespaceSegmentsToStrip.ContainsKey(area))
                    namespaceSegmentsToStrip.Add(area, new List<string>());
                
                var values = namespaceSegmentsToStrip[area].ToList();
                values.Add(namespaceSegment);
                namespaceSegmentsToStrip[area] = values;
            }

            return namespaceSegmentsToStrip;
        }
        
    }
}