using System;
using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// The exception that gets thrown when the <see cref="DefaultApplication"/> is requested, but no configuration details are given.
    /// </summary>
    public class ConfigurationNotSpecified : Exception
    {
        /// <summary>
        /// Instantiates an <see cref="ConfigurationNotSpecified"/> exception.
        /// </summary>
        public ConfigurationNotSpecified() 
            : base($"No configuration details has been specified.\n"+
             $"To get a default configuration for an {typeof(IApplication).AssemblyQualifiedName} you need to have specified an ApplicationName and BoundedContextName in a {JsonFile.DolittleJsonFileName} " +
             $"or provide the {typeof(DefaultApplication).AssemblyQualifiedName} with a {typeof(Config).AssemblyQualifiedName}")
        {

        }
    }
}