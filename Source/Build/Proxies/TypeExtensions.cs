using System;
using System.Linq;
using Dolittle.Applications.Configuration;
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
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsAPrimitiveType())
            {
                Activator.CreateInstance(type);
            }
            return null;
        }
        /// <summary>
        /// Returns a string that represents the namespace of the given <see cref="Type"/> where the NamespaceToStrip-segments are removed from the namespace
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string StripExcludedNamespaceSegments(this Type type, BoundedContextConfiguration config)
        {
            var area = new Area(){Value = type.Namespace.Split(".").First()};
            var segmentList = type.Namespace.Split(".").Skip(1).ToList();
            
            if (config.NamespaceSegmentsToStrip.ContainsKey(area))
            {
                foreach (var segment in config.NamespaceSegmentsToStrip[area]) 
                    segmentList.Remove(segment);
            }
            
            return string.Join(".", segmentList);
        }
    }
}