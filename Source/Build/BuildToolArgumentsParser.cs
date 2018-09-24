/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
            if (args.Length < 5 ) throw new ArgumentException($"The number of arguments to the Build Tool was not correct. It should be 5, it was {args.Length}");
            var assemblyPath = args[0];
            args = args.Skip(1).ToArray();
            var boundedContextConfigRelativePath = HandleBoundedContextConfigPath(args);
            var useModules = HandleUseModules(args);
            var namespaceSegmentsToStrip = HandleNamespaceSegmentsToStrip(args);
            var generateProxies = HandleGenerateProxies(args);
            var proxiesBasePath = HandleProxiesBasePath(args);

            return new BuildToolArgumentsParsingResult(assemblyPath, boundedContextConfigRelativePath, useModules, namespaceSegmentsToStrip, generateProxies, proxiesBasePath);
        }

        static string HandleBoundedContextConfigPath(string[] args)
        {
            return GetArgValue(args, "boundedContextConfigPath");
        }

        static bool HandleUseModules(string[] args)
        {
            var value = GetArgValue(args, "useModules");

            return ParseBoolValue(value);
        }

        static Dictionary<Area, IEnumerable<string>> HandleNamespaceSegmentsToStrip(string[] args)
        {
            var value = GetArgValue(args, "namespaceSegmentsToStrip");

            return ParseValueAsNamespaceSegmentsToStrip(value);
        }

        static bool HandleGenerateProxies(string[] args)
        {
            var value = GetArgValue(args, "generateProxies");

            return ParseBoolValue(value);
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
                    return match.Groups.Count == 3? match.Groups[2].Value.Trim() : "";
                
            }
            throw new ArgumentException($"The argument '{argumentName}' was was not present in the arguments to Dolittle Build Tool");
        }

        static bool ParseBoolValue(string value)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new ArgumentException("Error while parsing boolean value. The value is null or empty");

            var result = false;
            if (bool.TryParse(value, out result))
                return result;

            value = value.First().ToString().ToUpper();
            if (bool.TryParse(value, out result))
                return result;
            throw new ArgumentException("Error while parsing boolean value. The value was not a valid boolean value. It should be either True, true, False or false");
        }
        
        static Dictionary<Area, IEnumerable<string>> ParseValueAsNamespaceSegmentsToStrip(string value)
        {
            const char separator = '|';

            var namespaceSegmentsToStrip = new Dictionary<Area, IEnumerable<string>>();

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return namespaceSegmentsToStrip;

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