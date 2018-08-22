using System;
using System.Linq;
using Dolittle.Applications.Configuration;
using Dolittle.Reflection;

namespace Dolittle.Build.Proxies
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsAPrimitiveType())
            {
                Activator.CreateInstance(type);
            }
            return null;
        }
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