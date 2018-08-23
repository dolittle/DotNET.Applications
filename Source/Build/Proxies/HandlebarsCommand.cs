using System.Collections.Generic;
using Dolittle.Artifacts;
namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// The actual command proxy that's fed into the Handlebars templating engine
    /// </summary>
    public class HandlebarsCommand
    {
        /// <summary>
        /// Gets and sets the name of the Command
        /// </summary>
        public string CommandName {get; set;}
        /// <summary>
        /// Gets and sets the <see cref="ArtifactId"/> of the Command 
        /// </summary>
        /// <value></value>
        public string ArtifactId {get; set;}
        /// <summary>
        /// Gets and sets a list of <see cref="ProxyProperty"/> that represents the Command's properties
        /// </summary>
        public IList<ProxyProperty> Properties {get; set;} = new List<ProxyProperty>();
    }
}