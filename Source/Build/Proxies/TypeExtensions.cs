/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Dolittle.Applications.Configuration;
using Dolittle.Concepts;
using Dolittle.Reflection;


namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// A collection of usefull extensions for the <see cref="Type"/> class specific for Proxies
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the default value of a type
        /// </summary>
        /// <param name="type"></param>
        public static string GetDefaultValueAsString(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");  
            if (type.IsAPrimitiveType())
            {
                if (type.IsNullable()) return Nullable.GetUnderlyingType(type).GetDefaultValueAsString();
                if (type.Equals(typeof(DateTime)) || type.Equals(typeof(DateTimeOffset)) || type.Equals(typeof(TimeSpan))) return "new Date()";
                if (type.Equals(typeof(string)) || type.Equals(typeof(String))) return "''";
                if (type.Equals(typeof(Guid))) return $"'{Guid.Empty.ToString()}'";
                if (type.Equals(typeof(void))) return "{}";
                if (type.Equals(typeof(bool)) || type.Equals(typeof(Boolean))) return "false";
                if (type.IsEnum) return "0";

                return Activator.CreateInstance(type).ToString();
            }
            if (type.IsEnum) return "0";
            if (type.IsNullable()) return Nullable.GetUnderlyingType(type).GetDefaultValueAsString();
            if (type.IsConcept()) return type.GetConceptValueType().GetDefaultValueAsString();
            
            if (typeof(IEnumerable).IsAssignableFrom(type)) return "[]";
            if (typeof(IDictionary).IsAssignableFrom(type)) return "{}}";
            //TODO: Perhaps in the future we can generate an object with keys and default values when type is a class that can be represented as a key/value set
            return "{}";
        }
        /// <summary>
        /// Returns a string that represents the namespace of the given <see cref="Type"/> where the NamespaceToStrip-segments are removed from the namespace
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parsingResults"></param>
        /// <returns></returns>
        public static string StripExcludedNamespaceSegments(this Type type, ArgumentsParsingResult parsingResults)
        {
            var area = new Area(){Value = type.Namespace.Split(".").First()};
            var segmentList = type.Namespace.Split(".").Skip(1).ToList();
            
            if (parsingResults.NamespaceSegmentsToStrip.ContainsKey(area))
            {
                foreach (var segment in parsingResults.NamespaceSegmentsToStrip[area]) 
                    segmentList.Remove(segment);
            }
            
            return string.Join(".", segmentList);
        }
    }
}