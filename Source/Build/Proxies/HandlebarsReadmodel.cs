using System.Collections.Generic;
using Dolittle.Artifacts;
namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// The read model proxy that's fed into the Handlebars templating engine
    /// </summary>
    public class HandlebarsReadmodel
    {
        /// <summary>
        /// Gets and sets the name of the Readmodel
        /// </summary>
        public string ReadModelName {get; set;}
        /// <summary>
        /// Gets and sets the string representation of <see cref="ArtifactId"/> of the Readmodel 
        /// </summary>
        /// <value></value>
        public string ReadModelArtifactId {get; set;}
        /// <summary>
        /// Gets and sets the string representation of the readmodel generation
        /// </summary>
        public string ReadModelGeneration {get; set;}
        /// <summary>
        /// Gets and sets the list of <see cref="ProxyProperty"/>that represents the Properties or Arguments of the proxy
        /// </summary>
        public IEnumerable<ProxyProperty> Properties {get; set;} = new List<ProxyProperty>();
    }
}